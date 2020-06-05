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
    }

    //Unity, please, let me use enum
    public void DeePad(string direction)
    {
        switch(direction){
        
        case "forward":
            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.forward;
            break;
        case "right":
            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.reverse;
            break;
        case "left":
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.forward;
            break;
        case "reverse":     
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.reverse;
            break;
        case "brake":
            leftMotor.CurrentDir = Dir.brake;
            rightMotor.CurrentDir = Dir.brake;
            break;
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
