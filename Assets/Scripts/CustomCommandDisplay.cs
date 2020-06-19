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

    public static Dictionary<string, string> prefabPaths = new Dictionary<string, string>();

    private static string DataPath = string.Empty;

    void Awake()
    {
        DataPath = System.IO.Path.Combine(Application.persistentDataPath, "customCommands.xml");

        prefabPaths.Add("button", "Prefabs/CustomButtonPrefab");
        prefabPaths.Add("toggle", "Prefabs/CustomTogglePrefab");
        prefabPaths.Add("slider", "Prefabs/CustomSliderPrefab");
        prefabPaths.Add("text", "Prefabs/CustomTextPrefab");
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
        CustomCommand commandSpawned;
        
        commandSpawned = CreateCustomCommand(new Vector3(0.5f, 0.5f, 0), Quaternion.identity, type, _tempLabel, _tempID);

        switch(type)
        {
            case "button":
            commandSpawned.GetComponentInChildren<Button>().onClick.AddListener(commandSpawned.ButtonClick);
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

    public static CustomCommand CreateCustomCommand(Vector3 position, Quaternion rotation, string type, string label, int commandID)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPaths[type]);

        GameObject mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;
        
        CustomCommand customCommand = go.GetComponent<CustomCommand>() ?? go.AddComponent<CustomCommand>();
        //Set Parent not done in instantiate in order to maintain relative position
        go.transform.SetParent(mainCanvas.transform, false);

        customCommand.type = type;
        customCommand.commandID = commandID;
        customCommand.label = label;

        return customCommand;
    }

    public static CustomCommand CreateCustomCommand(CustomCommandData data, Vector3 position, Quaternion rotation)
    {
        GameObject prefab = Resources.Load<GameObject>(prefabPaths[data.type]);

        GameObject mainCanvas = GameObject.FindGameObjectWithTag("MainCanvas"); 
        GameObject go = GameObject.Instantiate(prefab, position, rotation) as GameObject;

        CustomCommand customCommand = go.GetComponent<CustomCommand>() ?? go.AddComponent<CustomCommand>();
        //Set Parent not done in instantiate in order to maintain relative position
        go.transform.SetParent(mainCanvas.transform, false);

        customCommand.data = data;

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