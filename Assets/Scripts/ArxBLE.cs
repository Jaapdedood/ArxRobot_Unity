using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ArxBLE : MonoBehaviour
{
    public GameObject Panel;
    public Text msg;

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
	private string _deviceAddress;
	private bool _foundSubscribeID = false;
	private bool _foundWriteID = false;
	private byte[] _dataBytes = null;
	private bool _rssiOnly = false;
	private int _rssi = 0;

    void Reset ()
	{
	   	_connected = false;
	   	_timeout = 0f;
        _state = States.None;
	    _deviceAddress = null;
	    _foundSubscribeID = false;
    	_foundWriteID = false;
	   	_dataBytes = null;
	    _rssi = 0;
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
		//Initialize(as central, as peripheral, Action, error Action)
		BluetoothLEHardwareInterface.Initialize (true, false, () => {
			
			SetState (States.Scan, 0.1f);
			
		}, (error) => {
			
			BluetoothLEHardwareInterface.Log ("Error during initialize: " + error);
		});
    }

    public void scanButton()
    {
        Panel.SetActive(true);

        msg.text = "Starting Scan\n";   
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
                    break;
                case States.Scan:
                /* ScanForPeripheralsWithServices (string[]serviceUUIDs, Action<string, string> action,
                 * Action<string, string, int, byte[]> actionAdvertisingInfo =null,
                 * bool rssiOnly = false, bool clearPeripheralList =true)
                 */
                BluetoothLEHardwareInterface.ScanForPeripheralsWithServices(ServiceUUIDs, OnDeviceDiscovered);
                    break;
                case  States.ScanRSSI:
                    break;
                case States.Connect:
                    break;
                case States.Subscribe:
                    break;
                case States.Unsubscribe:
                    break;
                case States.Disconnect:
                    break;
                }
            }
        }
    }

    void OnDeviceDiscovered(string name, string id){
        
    }

}
