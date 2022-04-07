using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthSystem : MonoBehaviour
{
    [SerializeField] ActionChannel _OnPlayerCollisionEventHandler;

    private void Awake()
    {
        _OnPlayerCollisionEventHandler.AddAction(() => Destroy(gameObject));
    }

    /*
     * Destroy(_player.gameObject);
     * Timer.StopTimer();
     */
}
