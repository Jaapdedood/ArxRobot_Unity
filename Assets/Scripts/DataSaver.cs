using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;
using System.IO;

public class DataSaver : MonoBehaviour
{
    public static CustomCommandContainer customCommandContainer = new CustomCommandContainer();

    public delegate void SerializeAction();
    //public static event SerializeAction OnLoaded;
    //public static event SerializeAction OnBeforeSave;

    public static void Load(string path)
    {
        customCommandContainer = LoadCustomCommands(path);

        foreach(CustomCommandData data in customCommandContainer.customCommands)
        {
            CustomCommandDisplay.CreateCustomCommand(data, new Vector3(data.posX, data.posY, data.posZ), Quaternion.identity);
        }

        //OnLoaded event listener workaround
        CustomCommand[] existingCommands = FindObjectsOfType<CustomCommand>();

        if(existingCommands != null)
        {
            for(int i = 0; i < existingCommands.Length; i++)
            {
                existingCommands[i].LoadData();
            }
        }

        //OnLoaded();

    }

    public static void RemoveCommandData(CustomCommandData data)
    {
        customCommandContainer.customCommands.Remove(data);
    }

    public static void Save(string path, CustomCommandContainer customCommands)
    {
            ClearCustomCommands();

            //OnBeforeSave() workaround
            CustomCommand[] existingCommands = FindObjectsOfType<CustomCommand>();

            if(existingCommands != null)
            {
                for(int i = 0; i < existingCommands.Length; i++)
                {
                    existingCommands[i].StoreData();
                    AddCustomCommandData(existingCommands[i].data);
                }
            }

            //OnBeforeSave();
            SaveCustomCommands(path, customCommands);
            ClearCustomCommands();
    }

    public static void AddCustomCommandData(CustomCommandData data)
    {
        customCommandContainer.customCommands.Add(data);
    }

    public static void ClearCustomCommands()
    {
        customCommandContainer.customCommands.Clear();
    }

    private static CustomCommandContainer LoadCustomCommands(string path)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CustomCommandContainer));

        FileStream stream = new FileStream(path, FileMode.Open);

        CustomCommandContainer customCommands = serializer.Deserialize(stream) as CustomCommandContainer;

        stream.Close();

        return customCommands;
    }

    private static void SaveCustomCommands(string path, CustomCommandContainer customCommands)
    {
        XmlSerializer serializer = new XmlSerializer(typeof(CustomCommandContainer));

        FileStream stream = new FileStream(path, FileMode.Create);

        serializer.Serialize(stream, customCommands);

        stream.Close();
    }
}
