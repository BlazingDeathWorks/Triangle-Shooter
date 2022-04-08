using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUpgradable
{
    public PowerData Data { get; }
    public void OnUpgrade();
}
