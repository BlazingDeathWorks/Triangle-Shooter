using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActionChannel_Bool", menuName = "SO Channels/Action Channel (Bool)")]
public class ActionChannel_Bool : ScriptableObject, IActionChannelGenerics1<Boolean>
{
    private Action<bool> func;

    public void AddAction(Action<bool> func)
    {
        this.func += func;
    }

    public void CallAction(bool var1)
    {
        func?.Invoke(var1);
    }

    public void RemoveAction(Action<bool> func)
    {
        this.func -= func;
    }

    public int GetSize()
    {
        return func.GetInvocationList().Length;
    }
}
