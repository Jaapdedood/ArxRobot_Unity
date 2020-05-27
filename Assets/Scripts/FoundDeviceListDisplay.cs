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

    public void Init(List<DeviceObject> foundDevices)
    {
        // Destroy what's already here
        for(int a = 0; a < targetTransform.childCount; a++)
        {
            Destroy(targetTransform.GetChild(a).gameObject);
        }

        // Spool out new list
        foreach(DeviceObject device in foundDevices){
            DeviceObjectDisplay display = (DeviceObjectDisplay)Instantiate(deviceDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Init(device);
        }
    }
}
