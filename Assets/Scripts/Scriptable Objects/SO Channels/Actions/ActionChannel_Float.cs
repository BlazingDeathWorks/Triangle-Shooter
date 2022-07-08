using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActionChannel_Float", menuName = "SO Channels/Action Channel (Float)")]
public class ActionChannel_Float : ScriptableObject, IActionChannelGenerics1<float>
{
    private Action<float> _func;

    public void AddAction(Action<float> func)
    {
        _func += func;
    }

    public void CallAction(float var1)
    {
        _func?.Invoke(var1);
    }

    public int GetSize()
    {
        throw new NotImplementedException();
    }

    public void RemoveAction(Action<float> func)
    {
        _func -= func;
    }
}
