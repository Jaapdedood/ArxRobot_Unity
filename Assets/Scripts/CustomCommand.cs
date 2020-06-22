using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Xml.Serialization;
using System.Text;

public class CustomCommand : MonoBehaviour
{
    public CustomCommandData data = new CustomCommandData();

    public string type;
    public string label;
    public int commandID;
    private string _textInput;

    public Text labelText;

    void Start()
    {
        labelText.text = label;
    }

    public void ButtonClick()
    {
        Debug.Log("Button | ID: " + commandID);

        ArxBLE.Instance.SendCommand((byte)commandID, null);
    }

    public void ToggleChanged(bool isOn)
    {
        Debug.Log("Toggle | ID: " + commandID + " | isOn: " + isOn);

        //Convert bool to 1 or 0
        byte data = isOn ? (byte) 1 : (byte) 0;
        
        ArxBLE.Instance.SendCommand((byte)commandID, data);
    }

    public void SliderChanged(float value)
    {
        Debug.Log("Slider | ID: " + commandID + " | value: " + value);
        ArxBLE.Instance.SendCommand((byte)commandID, (byte)value);
    }

    public void TextChanged(string input)
    {
        Debug.Log("Text | ID: " + commandID + " | input: " + input);
        _textInput = input;
    }

    public void TextClick()
    {
        Debug.Log("Text | ID: " + commandID + " | input: " + _textInput);
        byte[] textAsBytes = Encoding.ASCII.GetBytes(_textInput);
        ArxBLE.Instance.SendCommand((byte)commandID, textAsBytes);
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