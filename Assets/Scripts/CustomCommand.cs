using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Xml.Serialization;

public class CustomCommand : MonoBehaviour
{
    public CustomCommandData data = new CustomCommandData();

    public string type;
    public string label;
    public int commandID;

    public Text labelText;

    void Start()
    {
        labelText.text = label;
    }

    public void ButtonClick()
    {
        Debug.Log(commandID);
    }

    public void StoreData()
    {
        data.type = type;
        data.label = label;
        data.commandID = commandID;

        Vector3 pos = transform.position;
        data.posX = pos.x;
        data.posY = pos.y;
        data.posZ = pos.z;
    }

    public void LoadData()
    {
        type = data.type;
        label = data.label;
        commandID = data.commandID;
        
        transform.position = new Vector3(data.posX, data.posY, data.posZ);
        
    }

    void OnEnable()
    {
        // I can't get these to unsubscribe properly, which caused a huge headache. workaround in DataSaver.
        //DataSaver.OnLoaded += delegate {LoadData();};
        //DataSaver.OnBeforeSave += delegate {StoreData();};
        //DataSaver.OnBeforeSave += delegate {DataSaver.AddCustomCommandData(data);};
    }

    void OnDisable()
    {   
        // I can't get these to unsubscribe properly, which caused a huge headache. workaround in DataSaver.
        //DataSaver.OnLoaded -= delegate {LoadData();};
        //DataSaver.OnBeforeSave -= delegate {StoreData();};
        //DataSaver.OnBeforeSave += delegate { DataSaver.RemoveCommandData(data); };     
    }

    void OnDestroy()
    {

    }
}

public class CustomCommandData
{
    
    [XmlAttribute("Type")]
    public string type;

    [XmlAttribute("Label")]
    public string label;

    [XmlAttribute("ID")]
    public int commandID;

    [XmlAttribute("PosX")]
    public float posX;

    [XmlAttribute("PosY")]
    public float posY;

    [XmlAttribute("PosZ")]
    public float posZ;

}