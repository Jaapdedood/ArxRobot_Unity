using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsActions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Unity, please, let me use enum
    public void deePad(string direction)
    {
        switch(direction){
        
        case "forward":
            
            byte[] test = {0xCA, 0x05, 0x01, 0x01, 0xD5, 0x01, 0xD5, 0xCE};
            ArxBLE.Instance.SendByteArray(test);
            break;
        case "right":
            byte[] test2 = {0xCA, 0x05, 0x01, 0x01, 0xD5, 0x01, 0xD5, 0xCE};
            ArxBLE.Instance.SendByteArray(test2);
            break;
        }
        
    }
}
