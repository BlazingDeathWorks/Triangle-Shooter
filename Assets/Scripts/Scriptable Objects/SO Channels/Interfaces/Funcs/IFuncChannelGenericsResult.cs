using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public interface IFuncChannelGenericsResult<TResult>
{
    void AddAction(Func<TResult> func);
    void RemoveAction(Func<TResult> func);
    TResult CallAction();
    int GetSize();
}
