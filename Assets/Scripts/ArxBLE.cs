using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using System.Linq;

public class ArxBLE : MonoBehaviour
{
    public static ArxBLE Instance;

    public string msg;

    public string[] ServiceUUIDs = {"0000e0ff-3c17-d293-8e48-14fe2e4da212"};
    public string writeUUID = "0000ffe1-0000-1000-8000-00805f9b34fb";
    public string notifyUUID = "0000ffe2-0000-1000-8000-00805f9b34fb";

	enum States
	{
		None,
		Scan,
		ScanRSSI,
		Connect,
		Subscribe,
		Unsubscribe,
		Disconnect,
        CommSetup,
	}

    private bool _connected = false;
	private float _timeout = 0f;
	private States _state = States.None;
    private string _addressToConnect;
    private byte[] _dataBytes = null;
    private bool _foundNotifyUUID = false;
	private bool _foundWriteUUID = false;

    void Reset ()
	{
	   	_connected = false;
	   	_timeout = 0f;
        _state = States.None;
	}

	void SetState (States newState, float timeout)
	{
		_state = newState;
		_timeout = timeout;
	}
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

     // Start is called before the first frame update
    void Start()
    {
        Reset ();
    }

    public void ScanButtonClick()
    {
        msg = "Starting Scan\n";   

        //Initialize(as central, as peripheral, Action, error Action)
		BluetoothLEHardwareInterface.Initialize (true, false, () => {
			
			SetState (States.Scan, 0.1f);
			
		}, (error) => {
			
			BluetoothLEHardwareInterface.Log ("Error during initialize: " + error);
		});
    }

    public void ConnectButtonClick(string address)
    {
        BluetoothLEHardwareInterface.StopScan ();

        msg = "Connecting to: " + address;


        _addressToConnect = address;
        SetState (States.Connect, 0.5f);
    }

   public void SendByteArray(byte[] data)
	{
		// 6th Parameter is false since EH-MC17 doesn't support write with notify afaik
		BluetoothLEHardwareInterface.WriteCharacteristic (_addressToConnect, ServiceUUIDs[0], writeUUID, data, data.Length, false, (characteristicUUID) => {
            msg = "Byte sent: ";
            foreach(byte b in data){
                msg += b.ToString("X2") + " ";
            }
		});
	}

    public void SendCommand(byte command, byte[] data)
    {
        List<byte> list = new List<byte>();
        list.Add(command);

        if(data != null)
        {
            foreach(byte value in data)
            {
                list.Add(value);
            }
        }

        byte[] commandPacket = _commandPacketFromDataList(list);
        SendByteArray(commandPacket);
    }

    public void SendCommand(List<Byte> commandWithData)
    {
        byte[] commandPacket = _commandPacketFromDataList(commandWithData);
        SendByteArray(commandPacket);
    }

    private byte[] _commandPacketFromDataList(List<Byte> data)
    {   
        byte checkSum = 0;
        List<byte> commandPacket = new List<byte>();
        commandPacket.Add(CommandID.COMMAND_PACKET_ID);
        commandPacket.Add((byte)data.Count);
        commandPacket.AddRange(data);
        foreach(byte value in commandPacket){
            checkSum ^= value;
        }
        commandPacket.Add(checkSum);

        return commandPacket.ToArray();
    }
    
    bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID (uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID (uuid2);
		
		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}

    string FullUUID(string uuid)
	{
		return "0000" + uuid + "0000-1000-8000-00805f9b34fb";
	}

    void Update()
    {
        if(_timeout > 0f)
        {
            _timeout -= Time.deltaTime;
            if(_timeout <= 0f)
            {
                _timeout = 0f;

                switch(_state)
                {
                case States.None:
                    if(_dataBytes != null){
                    msg += "\nData Received: " + BitConverter.ToString(_dataBytes);
                    _dataBytes = null;
                    }
                    break;
                case States.Scan:
                    msg += "Devices Found: \n";
                    /* ScanForPeripheralsWithServices (string[]serviceUUIDs, Action<string, string> action,
                     * Action<string, string, int, byte[]> actionAdvertisingInfo =null,
                     * bool rssiOnly = false, bool clearPeripheralList =true)
                     */
                    BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(ServiceUUIDs, (address, name) => {
                        FoundDeviceList.DeviceInfoList.Add (new DeviceObject (address, name));
                        msg += name + "    " + address;
                        msg += "\n";
                        }
                    );
                    break;
                case  States.ScanRSSI:
                    //Not planning to support this any time soon
                    break;
                case States.Connect:
					// note that the first parameter is the address, not the name - error in API.
					BluetoothLEHardwareInterface.ConnectToPeripheral (_addressToConnect, null, null, (address, serviceUUID, characteristicUUID) => {

						if (IsEqual (serviceUUID, ServiceUUIDs[0]))
						{
							_foundNotifyUUID = _foundNotifyUUID || IsEqual (characteristicUUID, notifyUUID);
                            msg = ("_foundNotifyUUID: " + _foundNotifyUUID + " ");
							_foundWriteUUID = _foundWriteUUID || IsEqual (characteristicUUID, writeUUID);
                            msg += ("_foundWriteUUID: " + _foundWriteUUID);

							// if we have found both characteristics that we are waiting for
							// set the state. make sure there is enough timeout that if the
							// device is still enumerating other characteristics it finishes
							// before we try to subscribe
							if (_foundNotifyUUID && _foundWriteUUID)
							{
								_connected = true;
                                msg = "Connected Succesfully";
                                SetState (States.CommSetup, 2f);
							}
						}
					});
                    break;
                case States.Subscribe:
                    msg = "Subscribing..";
                	BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (_addressToConnect, ServiceUUIDs[0], notifyUUID, null, (address, characteristicUUID, bytes) => {
                        
                        // we don't have a great way to set the state other than waiting until we actually got
					    // some data back. For this demo with the rfduino that means pressing the button
					    // on the rfduino at least once before the GUI will update.
					    //_state = States.None;
                        SetState (States.None, 0.1f);

					    // we received some data from the device
					    _dataBytes = bytes;
                    });
                    msg = "Subscribed";
                    SetState (States.None, 0.1f);
                    break;
                case States.Unsubscribe:
                    break;
                case States.Disconnect:
                    break;
                case States.CommSetup:
                    msg = "Telling 3DoT to set up Comms...";
                    byte[] tempData = {0x00};
                    SendCommand(CommandID.COMM_SETUP, tempData);
                    SetState (States.Subscribe, 2f);
                    break;
                }
            }
        }
    }
}
