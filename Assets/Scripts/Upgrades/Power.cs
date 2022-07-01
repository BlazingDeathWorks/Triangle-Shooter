using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class Power : MonoBehaviour
{
    [SerializeField] private PowerData _powerData;
    [SerializeField] private GameObject _upgradable;
    private Button _button;
    private Image _image;
    private IUpgradableVariants _upgradableVariants;

    private void Awake()
    {
        _button = GetComponentInChildren<Button>();
        _image = GetComponentInChildren<Image>();

        _image.sprite = _powerData.Icon;

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
