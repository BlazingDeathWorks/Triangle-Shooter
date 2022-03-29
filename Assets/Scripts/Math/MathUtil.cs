using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class MathUtil
{
    public static float WrapFloat(float number, float min, float max)
    {
        if (number > max) number = min;
        if (number < min) number = max;
        return number;
    }
}
