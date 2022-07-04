using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class Power : MonoBehaviour
{
    public PowerData PowerData => _powerData;
    [SerializeField] private PowerData _powerData;
    [SerializeField] private GameObject _upgradableObject;
    private Button _button;
    private Image _image;
    private IUpgradable _upgradable;
    private IUpgradableVariants _upgradableVariants;
    private ShopController _shopController;

    private void Awake()
    {
        //INIT
        _shopController = GetComponentInParent<ShopController>();
        _button = GetComponentInChildren<Button>();
        _image = GetComponentInChildren<Image>();
        _upgradable = _upgradableObject.GetComponent<IUpgradable>();
        _upgradableVariants = _upgradableObject.gameObject.GetComponent<IUpgradableVariants>();

        _image.sprite = _powerData.Icon;
        _button.onClick.AddListener(() => _shopController.UpdatePower(this));
    }

    private void OnEnable()
    {
        if (_upgradableVariants == null) return;
        _upgradableVariants.Init(_powerData);
    }

    public void Upgrade()
    {
        _upgradable.OnUpgrade();
    }
}
