using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceObject
{
	public string Address;
	public string Name;
	public Image Image;

	public DeviceObject ()
	{
		Address = "";
		Name = "";
	}

	public DeviceObject (string address, string name)
	{
		Address = address;
		Name = name;
		//Image = some placeholder
	}

	public DeviceObject(string address, string name, Image image)
	{
		Address = address;
		Name = name;
		Image = image;
	}
}
