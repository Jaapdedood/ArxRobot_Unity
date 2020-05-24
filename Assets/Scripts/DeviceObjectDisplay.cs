using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceObjectDisplay : MonoBehaviour
{
    public Text deviceName;
    public Text deviceAddress;
    public Image deviceImage;

    public DeviceObject device;

    // Start is called before the first frame update
    void Start()
    {
        if(device != null)
        {
            Init(device);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Init(DeviceObject device)
    {
        this.device = device;
        if(deviceName != null)
        {
            deviceName.text = device.Name;
        }
        if(deviceAddress != null)
        {
            deviceAddress.text = device.Address;
        }
        if(deviceImage != null)
        {
            deviceImage = device.Image;
        }
    }
}
