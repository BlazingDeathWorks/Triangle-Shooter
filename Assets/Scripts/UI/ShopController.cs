using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private Tab _shopTab;
    [SerializeField][Range(0.1f, 1)] private float _timeScaleInShop = 0.5f;
    [SerializeField] private InputButton _shopInput;

    void Update()
    {
        if (_shopInput.Clicked)
        {
            if (_shopTab.gameObject.activeSelf)
            {
                _shopTab.gameObject.SetActive(false);
                Time.timeScale = 1;
                return;
            }
            _shopTab.Enable();
            Time.timeScale = _timeScaleInShop;
        }
    }
}
