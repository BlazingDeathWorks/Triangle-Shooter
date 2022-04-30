using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IActionChannelGenerics2<T1, T2>
{
    void AddAction(Action<T1, T2> func);
    void RemoveAction(Action<T1, T2> func);
    void CallAction(T1 var1, T2 var2);
    int GetSize();
}

