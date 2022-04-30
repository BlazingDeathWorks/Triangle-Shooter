using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Upgrade to increase max health of player
public class PlayerMaxHealthUpgradable : MonoBehaviour, IUpgradable
{
    public float MaxHealth { get; private set; } = 3;

    public void OnUpgrade()
    {
        if (MaxHealth % 5 == 0)
        {
            MaxHealth += 3;
        }
        else
        {
            MaxHealth += 2;
        }
        PlayerHealthSystem.Instance.UpdateHealth();
    }
}
