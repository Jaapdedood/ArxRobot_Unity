using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsDisplay : MonoBehaviour
{
    public Text msg;
    public float AlphaThreshold = 0.1f;
    public Image forwardButton;
    public Image rightButton;
    public Image reverseButton;
    public Image leftButton;
    public Image middleButton;
    public Slider leftTank;
    public Slider rightTank;

    void Start()
    {
        if(forwardButton != null){
            forwardButton.alphaHitTestMinimumThreshold = AlphaThreshold;
            rightButton.alphaHitTestMinimumThreshold = AlphaThreshold;
            reverseButton.alphaHitTestMinimumThreshold = AlphaThreshold;
            leftButton.alphaHitTestMinimumThreshold = AlphaThreshold;
            middleButton.alphaHitTestMinimumThreshold = AlphaThreshold;
        }
        GameObject TrashCan = GameObject.FindGameObjectWithTag("TrashCan");
        TrashCan.transform.localScale = new Vector3(0, 0, 0); //Cool trick to hide without disabling
    }

    public void ReturnHome(string whichSlider)
    {
        if(whichSlider == "left")
        {
            leftTank.value = 0.5f;
        }
        else if(whichSlider == "right")
        {
            rightTank.value = 0.5f;
        }
        else
        {
            Debug.Log("invalid slider param");
        }
    }

    public void ToggleDisplay(GameObject displayObject){
        displayObject.SetActive(!displayObject.activeSelf);
    }

    // Update is called once per frame
    void Update()
    {
        msg.text = ArxBLE.Instance.msg;
    }
}
