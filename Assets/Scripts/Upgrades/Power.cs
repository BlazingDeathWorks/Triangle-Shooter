using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class Power : MonoBehaviour
{
    [SerializeField] private PowerData _powerData;
    [SerializeField] private GameObject _upgradable;
    [SerializeField] private Button _button;
    [SerializeField] private Image _image;
    [SerializeField] private Text _nameText;
    private IUpgradableVariants _upgradableVariants;

    private void Awake()
    {
        if (_powerData == null || _upgradable == null || _image == null || _nameText == null || _button == null) return;

        _image.sprite = _powerData.Icon;
        _nameText.text = _powerData.Name;

        _upgradableVariants = _upgradable.gameObject.GetComponent<IUpgradableVariants>();

        IUpgradable upgradable;
        if (!_upgradable.TryGetComponent<IUpgradable>(out upgradable)) return;
        _button.onClick.AddListener(upgradable.OnUpgrade);
    }

    private void OnEnable()
    {
        if (_upgradableVariants == null) return;
        _upgradableVariants.Init(_powerData);
    }
}
