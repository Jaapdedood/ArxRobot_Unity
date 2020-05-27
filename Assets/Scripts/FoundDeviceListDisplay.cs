using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        foreach(DeviceObject device in foundDevices)
        {
            DeviceObjectDisplay display = (DeviceObjectDisplay)Instantiate(deviceDisplayPrefab);
            display.transform.SetParent(targetTransform, false);
            display.Init(device);

            //If I set onclick in inspector, it gets lost when saving as prefab, hence this is defined at runtime
            Button connectButton = display.GetComponentInChildren<Button>();
            connectButton.onClick.AddListener(test);
        }      
    }

    public void test(){
        Debug.Log("boop");
    }
}
