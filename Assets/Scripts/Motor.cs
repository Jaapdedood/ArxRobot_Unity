using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motor
{
    public int Speed;
    public Dir CurrentDir;

    public Motor()
    {
        Speed = 0;
        CurrentDir = Dir.release;
    }
    
    public Motor(int speed, Dir dir)
    {
        Speed = speed;
        CurrentDir = dir;
    }
}
