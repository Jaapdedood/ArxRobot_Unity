﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ArxBLE : MonoBehaviour
{
    public Text msg;
    public Text receivedData;

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

     // Start is called before the first frame update
    void Start()
    {
        Reset ();
    }

    public void ScanButtonClick()
    {
        msg.text = "Starting Scan\n";   

        //Initialize(as central, as peripheral, Action, error Action)
		BluetoothLEHardwareInterface.Initialize (true, false, () => {
			
			SetState (States.Scan, 0.1f);
			
		}, (error) => {
			
			BluetoothLEHardwareInterface.Log ("Error during initialize: " + error);
		});  
    }

    public void ConnectButtonClick(Text address)
    {
        BluetoothLEHardwareInterface.StopScan ();

        msg.text = "Connecting to: " + address.text;


        _addressToConnect = address.text;
        SetState (States.Connect, 0.5f);
    }

    bool IsEqual(string uuid1, string uuid2)
	{
		if (uuid1.Length == 4)
			uuid1 = FullUUID (uuid1);
		if (uuid2.Length == 4)
			uuid2 = FullUUID (uuid2);
		
		return (uuid1.ToUpper().CompareTo(uuid2.ToUpper()) == 0);
	}

    string FullUUID (string uuid)
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
                    msg.text += "Data Received: " + BitConverter.ToString(_dataBytes) + "\n";
                    _dataBytes = null;
                    }
                    break;
                case States.Scan:
                    msg.text += "Devices Found: \n";
                    /* ScanForPeripheralsWithServices (string[]serviceUUIDs, Action<string, string> action,
                     * Action<string, string, int, byte[]> actionAdvertisingInfo =null,
                     * bool rssiOnly = false, bool clearPeripheralList =true)
                     */
                    BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(ServiceUUIDs, (address, name) => {
                        FoundDeviceList.DeviceInfoList.Add (new DeviceObject (address, name));
                        msg.text += name + "    " + address;
                        msg.text += "\n";
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
							_foundWriteUUID = _foundWriteUUID || IsEqual (characteristicUUID, writeUUID);

							// if we have found both characteristics that we are waiting for
							// set the state. make sure there is enough timeout that if the
							// device is still enumerating other characteristics it finishes
							// before we try to subscribe
							if (_foundNotifyUUID && _foundWriteUUID)
							{
								_connected = true;
								SetState (States.Subscribe, 2f);
							}
						}
					});
                    break;
                case States.Subscribe:
                	BluetoothLEHardwareInterface.SubscribeCharacteristicWithDeviceAddress (_addressToConnect, ServiceUUIDs[0], notifyUUID, null, (address, characteristicUUID, bytes) => {

					    // we don't have a great way to set the state other than waiting until we actually got
					    // some data back. For this demo with the rfduino that means pressing the button
					    // on the rfduino at least once before the GUI will update.
					    _state = States.None;

					    // we received some data from the device
					    _dataBytes = bytes;
					});
                    break;
                case States.Unsubscribe:
                    break;
                case States.Disconnect:
                    break;
                }
            }
        }
    }

}
