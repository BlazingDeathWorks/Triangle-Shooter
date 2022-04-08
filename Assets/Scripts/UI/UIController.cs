using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] private Tab _shopTab;
    [SerializeField] private InputButton _shopInput;

    void Update()
    {
        if (_shopInput.Clicked)
        {
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                return;
            }
            _shopTab.Enable();
        }
    }
}
