using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBuildSystem : MonoBehaviour, IUpgradable
{
    [SerializeField] private ActionChannel_Bool _shopActivatedEventHandler;
    [SerializeField] private ActionChannel_Bool _buildActivatedEventHandler;
    [SerializeField] private InputButton _buildInput;
    [SerializeField] private BuildGridBox _gridBoxPrefab;
    [SerializeField] private Transform _gridContainer;
    [SerializeField] private float _slowTime = 0.3f;
    private int _dimension = 3;
    private int _absPreviousDimension = 3;
    private Transform _transform;

    private void Awake()
    {
        _shopActivatedEventHandler?.AddAction(OnShopActivated);
        _transform = transform;
        _gridContainer.gameObject.SetActive(false);
        CreateGrid();
    }

    private void Update()
    {
        if (_buildInput.Clicked)
        {
            _gridContainer.position = _transform.position;
            _gridContainer.gameObject.SetActive(!_gridContainer.gameObject.activeSelf);
            _buildActivatedEventHandler?.CallAction(_gridContainer.gameObject.activeSelf);
            Time.timeScale = _gridContainer.gameObject.activeSelf ? _slowTime : 1;
        }
    }

    private void OnShopActivated(bool gonnaBeActive)
    {
        if (gonnaBeActive)
        {
            _gridContainer.gameObject.SetActive(false);
            _buildActivatedEventHandler.CallAction(_gridContainer.gameObject.activeSelf);
            gameObject.SetActive(false);
            return;
        }
        gameObject.SetActive(true);
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
                BuildGridBox instance = Instantiate(_gridBoxPrefab, new Vector3(x + _transform.position.x, y + _transform.position.y, 0), Quaternion.identity);
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
        _absPreviousDimension = _dimension;
        CreateGrid();
    }
}
