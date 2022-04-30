using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Tab _shopTab;
    [SerializeField] private InputButton _shopInput;

    void Update()
    {
        if (_shopInput.Clicked)
        {
            if (_shopTab.gameObject.activeSelf)
            {
                _shopTab.gameObject.SetActive(false);
                return;
            }
            _shopTab.Enable();
        }
    }
}
