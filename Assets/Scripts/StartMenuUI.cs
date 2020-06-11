using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartMenuUI : MonoBehaviour
{
    public GameObject PrepareBotCanvas;
    public Text msg;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        msg.text = ArxBLE.Instance.msg;
    }


    public void TogglePrepareBotCanvas(){
        PrepareBotCanvas.SetActive(!PrepareBotCanvas.activeSelf);
    }

    public void OpenGettingStarterURL(){
        Application.OpenURL("https://www.arxterra.com/getting-started/");
    }
}
