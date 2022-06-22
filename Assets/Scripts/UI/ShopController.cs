using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopController : MonoBehaviour
{
    [SerializeField] private ActionChannel_Bool _shopActivatedEventHandler;
    [SerializeField] private Tab _shopTab;
    [SerializeField] private InputButton _shopInput;

    void Update()
    {
        //You can just delete this if statement when you need it
        if (_shopInput.Clicked)
        {
            _shopActivatedEventHandler?.CallAction(!_shopTab.gameObject.activeSelf);
            if (_shopTab.gameObject.activeSelf)
            {
                _shopTab.gameObject.SetActive(false);
                Time.timeScale = 1;
                return;
            }
            Time.timeScale = 0;
            _shopTab.Enable();
        }
    }
}
