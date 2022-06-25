using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerData", menuName = "Upgrades/Power Data", order = 1)]
public class PowerData : ScriptableObject
{
    public Sprite Icon;
    public string Name;
    [TextArea(3, 5)] public string Description;
}
