using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlsActions : MonoBehaviour
{
    private Motor leftMotor = new Motor();
    private Motor rightMotor = new Motor();

    // Start is called before the first frame update
    void Start()
    {
        leftMotor.CurrentDir = Dir.forward;
        leftMotor.Speed = 125;
        List<byte> test = new List<byte>();
        test.Add((byte)leftMotor.CurrentDir);
        test.Add((byte)leftMotor.CurrentDir);
        test.Add((byte)leftMotor.Speed);

        foreach(byte value in test){
            Debug.Log(value.ToString("X2"));
        }
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
            //byte[] test = {0xA5, 0x05, 0x01, 0x01, 0xD5, 0x01, 0xD5, 0xA1};
            //ArxBLE.Instance.SendByteArray(test);
            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.forward;

            ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
            break;
        case "right":
            //byte[] test2 = {0xA5, 0x05, 0x01, 0x01, 0xD5, 0x01, 0xD5, 0xA1};
            //ArxBLE.Instance.SendByteArray(test2);

            leftMotor.CurrentDir = Dir.forward;
            rightMotor.CurrentDir = Dir.reverse;

            ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
            break;
        case "left":
        
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.forward;

            ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
            break;
        case "reverse":
        
            leftMotor.CurrentDir = Dir.reverse;
            rightMotor.CurrentDir = Dir.reverse;

            ArxBLE.Instance.SendCommand(MotorsToList(leftMotor, rightMotor));
            break;
        }
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
