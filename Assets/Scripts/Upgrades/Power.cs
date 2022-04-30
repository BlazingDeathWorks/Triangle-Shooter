using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Power : MonoBehaviour
{
    [SerializeField] private PowerData _powerData;
    [SerializeField] private GameObject _upgradable;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Text _nameText;
    [SerializeField] private Text _costText;

    private void Awake()
    {
        if (_powerData == null || _upgradable == null || _image == null || _nameText == null || _costText == null || _button == null) return;

        _image.sprite = _powerData.Icon;
        _nameText.text = _powerData.Name;
        _costText.text = $"{_powerData.Cost.ToString()} G";

        IUpgradable upgradable;
        if (!_upgradable.TryGetComponent<IUpgradable>(out upgradable)) return;
        _button.onClick.AddListener(upgradable.OnUpgrade);
    }
}
