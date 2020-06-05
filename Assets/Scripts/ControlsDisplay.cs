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

    void Start()
    {
        forwardButton.alphaHitTestMinimumThreshold = AlphaThreshold;
        rightButton.alphaHitTestMinimumThreshold = AlphaThreshold;
        reverseButton.alphaHitTestMinimumThreshold = AlphaThreshold;
        leftButton.alphaHitTestMinimumThreshold = AlphaThreshold;
        middleButton.alphaHitTestMinimumThreshold = AlphaThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        msg.text = ArxBLE.Instance.msg;
    }
}
