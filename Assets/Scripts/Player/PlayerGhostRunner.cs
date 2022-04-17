using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerGhostRunner : MonoBehaviour
{
    [SerializeField] private float _timeBetweenGhost = 1;
    private PlayerMovementController _playerMovementController;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    private void Update()
    {
        if (!_playerMovementController.IsMoving) return;

    }
}
