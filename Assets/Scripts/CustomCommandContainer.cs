using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml.Serialization;

[XmlRoot("CustomCommandCollection")]
public class CustomCommandContainer
{
    [XmlArray("CustomCommands")]
    [XmlArrayItem("CustomCommand")]
    public List<CustomCommandData> customCommands = new List<CustomCommandData>();
}
