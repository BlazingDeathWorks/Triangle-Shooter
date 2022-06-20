using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildSystem : MonoBehaviour, IUpgradable
{
    [SerializeField] private ActionChannel_Bool _buildActivatedEventHandler;
    [SerializeField] private InputButton _buildInput;
    [SerializeField] private BuildGridBox _gridBoxPrefab;
    [SerializeField] private Transform _gridContainer;
    private int _dimension = 3;
    private int _absPreviousDimension = 3;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
        _gridContainer.gameObject.SetActive(false);
        CreateGrid();
    }

    private void Update()
    {
        if (_buildActivatedEventHandler == null) return;
        if (_buildInput.Clicked)
        {
            _gridContainer.position = _transform.position;
            _gridContainer.gameObject.SetActive(!_gridContainer.gameObject.activeSelf);
            _buildActivatedEventHandler.CallAction(_gridContainer.gameObject.activeSelf);
            Time.timeScale = _gridContainer.gameObject.activeSelf ? 0 : 1;
        }
    }

    //Creates Build Grid Boxes
    private void CreateGrid()
    {
        int x = -_dimension / 2;
        int y = -_dimension / 2;
        for (int i = 0; i < _dimension; i++)
        {
            if (Mathf.Abs(x) > _absPreviousDimension)
            {
                x++;
                continue;
            }
            for (int j = 0; j < _dimension; j++)
            {
                if (i == _dimension / 2 && j == _dimension / 2)
                {
                    y++;
                    continue;
                }
                BuildGridBox instance = Instantiate(_gridBoxPrefab, new Vector3(x, y, 0), Quaternion.identity);
                instance.transform.parent = _gridContainer;
                y++;
            }
            x++;
            y = -_dimension / 2;
        }
    }

    public void OnUpgrade()
    {
        _dimension += 2;
        CreateGrid();
        _absPreviousDimension = _dimension;
    }
}
