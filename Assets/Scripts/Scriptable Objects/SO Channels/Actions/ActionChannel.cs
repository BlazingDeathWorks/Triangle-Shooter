using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[CreateAssetMenu(fileName = "NewActionChannel", menuName = "SO Channels/Action Channel")]
public class ActionChannel : ScriptableObject
{
    Action func;

    public void AddAction(Action func)
    {
        this.func += func;
    }
    public void RemoveAction(Action func)
    {
        this.func -= func;
    }
    public void CallAction()
    {
        func?.Invoke();
    }
    public int GetSize()
    {
        return func.GetInvocationList().Length;
    }
    public void ClearAll()
    {
        func = null;
    }
}

