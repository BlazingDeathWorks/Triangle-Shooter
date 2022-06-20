using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActionChannel_Bool", menuName = "SO Channels/Action Channel (Bool)")]
public class ActionChannel_Bool : ScriptableObject, IActionChannelGenerics1<bool>
{
    private Action<bool> _func;

    public void AddAction(Action<bool> func)
    {
        _func += func;
    }

    public void CallAction(bool var1)
    {
        _func?.Invoke(var1);
    }

    public int GetSize()
    {
        throw new NotImplementedException();
    }

    public void RemoveAction(Action<bool> func)
    {
        _func -= func;
    }
}
