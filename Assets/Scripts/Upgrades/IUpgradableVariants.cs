using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//How to improve: Make a validator (Odin) that checks T to be either float or int or possibly Unit Test
public interface IUpgradableVariants
{
    public object BonusFactor { get; set; }
    public void Init(PowerData data);
}