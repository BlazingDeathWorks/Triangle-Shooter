using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public interface IActionChannelGenerics1<T1>
{
    void AddAction(Action<T1> func);
    void RemoveAction(Action<T1> func);
    void CallAction(T1 var1);
}

