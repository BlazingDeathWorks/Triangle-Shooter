using System;
using UnityEngine;

[CreateAssetMenu(fileName = "NewActionChannel_Int", menuName = "SO Channels/Action Channel (Int)")]
public class ActionChannel_Int : ScriptableObject, IActionChannelGenerics1<int>
{
    private Action<int> _func;

    public void AddAction(Action<int> func)
    {
        _func += func;
    }

    public void CallAction(int var1)
    {
        _func?.Invoke(var1);
    }

    public int GetSize()
    {
        throw new NotImplementedException();
    }

    public void RemoveAction(Action<int> func)
    {
        _func -= func;
    }
}
