using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoundDeviceListDisplay : MonoBehaviour
{
    public Transform targetTransform;
    public DeviceObjectDisplay deviceDisplayPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Prime(List<DeviceObject> foundDevices)
    {
        foreach(DeviceObject device in foundDevices){
            DeviceObjectDisplay display = (DeviceObjectDisplay)Instantiate(deviceDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Init(device);
        }
    }
}
