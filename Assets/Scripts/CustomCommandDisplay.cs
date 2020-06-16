using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCommandDisplay : MonoBehaviour
{
    public Transform Canvas;
    public Button CustomButtonPrefab;
    public string TempCommandString;
    private Button _customButton;
    private int _tempID;
    private string _tempLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawner(string type)
    {
        //toggle off current panel
        //currentPanel.SetActive(!currentPanel.activeSelf);

        switch(type)
        {
            case "button":
            	_customButton = (Button)Instantiate(CustomButtonPrefab);
                _customButton.transform.SetParent(Canvas, false);

                CustomCommand customCommandScript = _customButton.GetComponent<CustomCommand>();

                _customButton.GetComponentInChildren<Button>().onClick.AddListener(customCommandScript.ButtonClick);
                Debug.Log("Setting commandscript tempID:");
                Debug.Log(_tempID);
                customCommandScript.CommandID = _tempID;
                _customButton.GetComponentInChildren<Text>().text = _tempLabel;
            break;
            case "toggle":
            break;
            case "slider":
            break;
            case "text":
            break;
        }
    }
    //Unity Inspector only allows string
    public void CommandIDEntered(string value)
    {
        _tempID = Convert.ToInt32(value);
    }

    public void CommandLabelEntered(string label)
    {
        _tempLabel = label;
    }
}
