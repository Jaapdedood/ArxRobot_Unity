using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsActions : MonoBehaviour
{
    private Motor leftMotor = new Motor();
    private Motor rightMotor = new Motor();
    private string _lastAction;


    // Start is called before the first frame update
    void Start()
    {
        leftMotor.Speed = 150;
        rightMotor.Speed = 150;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void SliderChanged(float sliderValue)
    {
        leftMotor.Speed = (int)sliderValue;
        rightMotor.Speed = (int)sliderValue;
        DeePad(_lastAction);
    }

    public void LeftTank(float sliderValue)
    {
        if(sliderValue >= 0)
        {
            leftMotor.CurrentDir = Dir.forward;
            leftMotor.Speed = (int)sliderValue;
        }
        else
        {
            leftMotor.CurrentDir = Dir.reverse;
            sliderValue *= -1;
            leftMotor.Speed = (int)sliderValue;
        }

        if(ControlsSettings.Instance.reverseLeft)
        {
            if(leftMotor.CurrentDir == Dir.forward)
            {
                leftMotor.CurrentDir = Dir.reverse;
            }
            else if(leftMotor.CurrentDir == Dir.reverse)
            {
                leftMotor.CurrentDir = Dir.forward;
            }
        }
        ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
    }

    public void RightTank(float sliderValue)
    {
            if(sliderValue >= 0)
        {
            rightMotor.CurrentDir = Dir.forward;
            rightMotor.Speed = (int)sliderValue;
        }
        else
        {
            rightMotor.CurrentDir = Dir.reverse;
            sliderValue *= -1;
            rightMotor.Speed = (int)sliderValue;
        }

        if(ControlsSettings.Instance.reverseRight)
        {
            if(rightMotor.CurrentDir == Dir.forward)
            {
                rightMotor.CurrentDir = Dir.reverse;
            }
            else if(rightMotor.CurrentDir == Dir.reverse)
            {
                rightMotor.CurrentDir = Dir.forward;
            }
        }
        ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));            
    }

    public void DeePad(string direction)
    {
        switch(direction){
        
        case "forward":
            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.forward;
            _lastAction = "forward";
            break;
        case "right":
            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.reverse;
            _lastAction = "right";
            break;
        case "left":
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.forward;
            _lastAction = "left";
            break;
        case "reverse":     
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.reverse;
            _lastAction = "reverse";
            break;
        case "brake":
            leftMotor.CurrentDir = Dir.brake;
            rightMotor.CurrentDir = Dir.brake;
            _lastAction = "brake";
            break;
        }
        if(ControlsSettings.Instance.reverseLeft)
        {
            if(leftMotor.CurrentDir == Dir.forward)
            {
                leftMotor.CurrentDir = Dir.reverse;
            }
            else if(leftMotor.CurrentDir == Dir.reverse)
            {
                leftMotor.CurrentDir = Dir.forward;
            }
        }
        if(ControlsSettings.Instance.reverseRight)
        {
            if(rightMotor.CurrentDir == Dir.forward)
            {
                rightMotor.CurrentDir = Dir.reverse;
            }
            else if(rightMotor.CurrentDir == Dir.reverse)
            {
                rightMotor.CurrentDir = Dir.forward;
            }
        }
        ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
    }

    private List<byte> MotorsToList(Motor leftMotor, Motor rightMotor)
    {
        /*
         * "[byte array index] descripton
         * [0] = 0x01 (MOVE)
         * [1] = (unsigned byte) Left Run Mode
         * [2] = (unsigned byte) Left Speed
         * [3] = (unsigned byte) Right Run Mode
         * [4] = (unsigned byte) Right Speed"
        */
        List<byte> returnList = new List<byte>();
        returnList.Add(0x01);
        returnList.Add((byte)leftMotor.CurrentDir);
        returnList.Add((byte)leftMotor.Speed);
        returnList.Add((byte)rightMotor.CurrentDir);
        returnList.Add((byte)rightMotor.Speed);
        
        return returnList;
    }
}
