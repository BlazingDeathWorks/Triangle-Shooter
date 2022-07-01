using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradableVariants
{
    public float BonusFactor { get; set; }
    public void Init(PowerData data);
}
