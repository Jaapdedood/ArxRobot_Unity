using UnityEngine;
using System.Collections.Generic;

public class FoundDeviceList : MonoBehaviour
{
	static public List<DeviceObject> DeviceInfoList = new List<DeviceObject>();

	// Use this for initialization
	void Start ()
	{
		DontDestroyOnLoad (gameObject);
	}
}
