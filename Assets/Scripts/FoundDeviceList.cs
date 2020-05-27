using UnityEngine;
using System.Collections.Generic;

public class FoundDeviceList : MonoBehaviour
{
	public Transform Canvas;
	public List<DeviceObject> debugDevicesToAdd;
	static public List<DeviceObject> DeviceInfoList = new List<DeviceObject>();
	public FoundDeviceListDisplay deviceListPrefab;
	private FoundDeviceListDisplay displayer;


	// Use this for initialization
	void Start ()
	{
		debugAddDevice(debugDevicesToAdd);
		displayer = (FoundDeviceListDisplay)Instantiate(deviceListPrefab);
		displayer.transform.SetParent(Canvas, false);
		displayer.Init(DeviceInfoList);

		DontDestroyOnLoad (gameObject);
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
	}
}
