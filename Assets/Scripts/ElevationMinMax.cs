using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevationMinMax
{
    public float Min { get; private set; }
    public float Max { get; private set; }
    public ElevationMinMax()
    {
        Min = float.MaxValue;
        Max = float.MinValue; 
    }
    public void UpdateMinMax(float value)
    {
        if(value > Max)
        {
            Max = value;
        }
        else if(value < Min)
        {
            Min = value;
        }
    }
}
