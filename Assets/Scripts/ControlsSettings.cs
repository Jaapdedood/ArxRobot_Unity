using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSettings : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ToggleAction(string setting, bool value)
    {
        switch(setting)
        {
            case "reverseLeftMotor":
                Debug.Log("It worked");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
