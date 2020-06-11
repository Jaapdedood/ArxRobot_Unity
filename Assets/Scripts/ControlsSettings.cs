using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsSettings : MonoBehaviour
{
    public static ControlsSettings Instance;

    public bool reverseLeft;
    public bool reverseRight;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        reverseLeft = false;
        reverseRight = false;
    }

    public void ToggleAction(string setting, bool value)
    {
        switch(setting)
        {
            case "reverseLeft":
                reverseLeft = !reverseLeft;
                break;
            case "reverseRight":
                reverseRight = !reverseRight;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
