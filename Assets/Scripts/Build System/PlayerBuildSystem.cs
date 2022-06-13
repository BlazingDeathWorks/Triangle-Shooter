using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlayerBuildSystem : MonoBehaviour
{
    [SerializeField] private InputButton _buildInput;
    [SerializeField] private Transform _buildBlockBaseContainer;
    [SerializeField] private BuildingBlock _buildingBlock;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (_buildInput.Clicked)
        {
            //Later
            return;
        }
    }
}
