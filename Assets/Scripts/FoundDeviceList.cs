using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class FoundDeviceList : MonoBehaviour
{
	public Transform Canvas;
	public List<DeviceObject> debugDevicesToAdd;
	static public List<DeviceObject> DeviceInfoList = new List<DeviceObject>();
	public FoundDeviceListDisplay deviceListPrefab;
	private FoundDeviceListDisplay displayer;
	private bool _connectButtonClicked = false;

	// Use this for initialization
	void Start ()
	{
		//Drag and drop UnknownDevice prefabs into debugDevicesToAdd in the Unity Editor and uncomment below to test without BLE
		//debugAddDevice(debugDevicesToAdd);

		GameObject ScanButton = GameObject.FindGameObjectWithTag("ScanButton");
		ScanButton.GetComponentInChildren<Button>().onClick.AddListener(scanButtonClick);
		
		if(!ArxBLE.Instance.connected)
		{
		ArxBLE.Instance.ScanButtonClick();
		}
	}

	void debugAddDevice(List<DeviceObject> devices)
	{
		foreach(DeviceObject device in devices)
		{
			DeviceInfoList.Add(device);
		}
		
	}

	public void runDisplayer()
	{
		displayer.Init(DeviceInfoList);
		
		//Have to do all this at runtime as devices are spawned at runtime
		GameObject[] spawnedDevices = GameObject.FindGameObjectsWithTag("Device");
		GameObject[] connectButtons = GameObject.FindGameObjectsWithTag("ConnectButton");
				
		for(int a = 0; a < connectButtons.Length; a++)
		{
			string address =  spawnedDevices[a].transform.Find("Address").GetComponent<Text>().text;
			connectButtons[a].GetComponentInChildren<Button>().onClick.AddListener(delegate{connectButtonClick(address);});
		}
	}

	public void scanButtonClick()
	{
		if(GameObject.Find("DeviceListUI(Clone)") != null)
		{
        	Destroy(GameObject.Find("DeviceListUI(Clone)"));
		}

		displayer = (FoundDeviceListDisplay)Instantiate(deviceListPrefab);
		displayer.transform.SetParent(Canvas, false);
		displayer.Init(DeviceInfoList);
		runDisplayer();
	}

	public void connectButtonClick(string address)
	{
		ArxBLE.Instance.ConnectButtonClick(address);
		_connectButtonClicked = true;
	}

	void Update()
	{
		if(ArxBLE.Instance.connected && _connectButtonClicked)
		{
			_connectButtonClicked = false;
			SceneManager.LoadScene("ControlSelection");
		}
	}
}
