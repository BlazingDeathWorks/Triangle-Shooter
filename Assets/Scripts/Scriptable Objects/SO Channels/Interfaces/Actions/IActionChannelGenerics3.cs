using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IActionChannelGenerics3<T1, T2, T3>
{
    void AddAction(Action<T1, T2, T3> func);
    void RemoveAction(Action<T1, T2, T3> func);
    void CallAction(T1 var1, T2 var2, T3 var3);
}

