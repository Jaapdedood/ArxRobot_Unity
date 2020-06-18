using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CustomCommandDisplay : MonoBehaviour
{
    //public Transform Canvas;
    
    private Button _customButton;
    private int _tempID;
    private string _tempLabel;

    public Button SaveButton;
    public Button LoadButton;
    public const string ButtonPrefab = "Prefabs/CustomButtonPrefab";

    private static string DataPath = string.Empty;

    void Awake()
    {
        DataPath = System.IO.Path.Combine(Application.persistentDataPath, "customCommands.xml");
    }

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
            /*
            	_customButton = (Button)Instantiate(CustomButtonPrefab);
                _customButton.transform.SetParent(Canvas, false);

                CustomCommand customCommandScript = _customButton.GetComponent<CustomCommand>();

                _customButton.GetComponentInChildren<Button>().onClick.AddListener(customCommandScript.ButtonClick);
                Debug.Log("Setting commandscript tempID:");
                Debug.Log(_tempID);
                customCommandScript.commandID = _tempID;
                _customButton.GetComponentInChildren<Text>().text = _tempLabel;
                */
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

    public static CustomCommand CreateCustomCommand(string path, Vector3 position, Quaternion rotation, int commandID)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        GameObject go = GameObject.Instantiate(prefab, position, rotation, mainCanvas.transform) as GameObject;

        CustomCommand customCommand = go.GetComponent<CustomCommand>() ?? go.AddComponent<CustomCommand>();

        customCommand.commandID = commandID;

        return customCommand;
    }

    public static CustomCommand CreateCustomCommand(CustomCommandData data, string path, Vector3 position, Quaternion rotation, int commandID)
    {
        GameObject prefab = Resources.Load<GameObject>(path);

        GameObject mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas"); 
        GameObject go = GameObject.Instantiate(prefab, position, rotation, mainCanvas.transform) as GameObject;

        CustomCommand customCommand = go.GetComponent<CustomCommand>() ?? go.AddComponent<CustomCommand>();

        customCommand.data = data;

        customCommand.commandID = commandID;

        return customCommand;
    }

    public void DestroyExisting()
    {
        CustomCommand[] existingCommands = FindObjectsOfType<CustomCommand>();

        if(existingCommands != null)
        {
            for(int i = 0; i < existingCommands.Length; i++)
            {
                Destroy(existingCommands[i].gameObject);
            }
        }
    }

    public void SaveIfExist()
    {
        CustomCommand[] existingCommands = FindObjectsOfType<CustomCommand>();

        if(existingCommands.Length > 0)
        {
            DataSaver.Save(DataPath, DataSaver.customCommandContainer);
        }
    }

    void OnEnable()
    {   
        LoadButton.onClick.AddListener(delegate{DestroyExisting();});
        SaveButton.onClick.AddListener(delegate{SaveIfExist();});
        LoadButton.onClick.AddListener(delegate{DataSaver.Load(DataPath);});
    }

    void OnDisable()
    {
        LoadButton.onClick.RemoveListener(delegate{DestroyExisting();});
        SaveButton.onClick.RemoveListener(delegate{SaveIfExist();});
        LoadButton.onClick.RemoveListener(delegate{DataSaver.Load(DataPath);});
    }
}