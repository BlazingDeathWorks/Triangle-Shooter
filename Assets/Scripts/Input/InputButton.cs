using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KeyType { Up, Down, Hold }
[System.Serializable]
public class InputButton
{
    public bool Disabled = false;
    public KeyCode KeyCode;
    public KeyType KeyType;

    public bool Clicked
    {
        get
        {
            if (Disabled) return false;
            switch (KeyType)
            {
                case KeyType.Up:
                    return Input.GetKeyUp(KeyCode);
                case KeyType.Down:
                    return Input.GetKeyDown(KeyCode);
                case KeyType.Hold:
                    return Input.GetKey(KeyCode);
                default:
                    return false;
            }
        }
    }
}
