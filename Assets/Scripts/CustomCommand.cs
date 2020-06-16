using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCommand : MonoBehaviour
{
    public int CommandID;
    // Start is called before the first frame update
    void Start()
    {
        //CommandID = 0;
    }

    private void Awake()
    {
        
    }

    public void ButtonClick()
    {
        Debug.Log(CommandID);
    }
}
