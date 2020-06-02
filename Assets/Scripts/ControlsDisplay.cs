using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDisplay : MonoBehaviour
{
    public Text msg;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        msg.text = ArxBLE.Instance.msg;
    }
}
