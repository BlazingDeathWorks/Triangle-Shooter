using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewPowerData", menuName = "Upgrades/Power Data", order = 1)]
public class PowerData : ScriptableObject
{
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _name;

    public Sprite Icon => _icon;
    public string Name => _name;
}
