using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewFuncChannelResult_Bool", menuName = "SO Channels/Func Channel Result (Bool)")]
public class FuncChannel_Bool : ScriptableObject, IFuncChannelGenericsResult<Boolean>
{
    private Func<bool> func;

    public void AddAction(Func<bool> func)
    {
        this.func += func;
    }
    public void RemoveAction(Func<bool> func)
    {
        this.func -= func;
    }
    public bool CallAction()
    {
        return func.Invoke();
    }
    public int GetSize()
    {
        return func.GetInvocationList().Length;
    }

}
