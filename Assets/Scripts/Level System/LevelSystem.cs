using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private ActionChannel _playerDiedEventHandler;
    [SerializeField] private ActionChannel_Int _leveledUpEventHandler;
    [SerializeField] private Text _levelText;
    [SerializeField] Slider expText = null;
    [SerializeField] float xp = 0;
    [SerializeField] float limit = 10;
    [SerializeField] ActionChannel giveXp;
    [SerializeField] private ActionChannel_Bool _shopActivatedEventHandler;
    [SerializeField] private GameObject _shopTab;
    [SerializeField] private InputButton _shopInput;
    private int _level = 1;

    private void Awake()
    {
        expText.value = xp/limit;
        giveXp.AddAction(UpdateXp);
        _shopActivatedEventHandler?.AddAction(OnShopActivated);
        _playerDiedEventHandler?.AddAction(UpdateLevelText);
    }

    private void OnDestroy()
    {
        _shopActivatedEventHandler?.RemoveAction(OnShopActivated);
        _playerDiedEventHandler?.RemoveAction(UpdateLevelText);
        giveXp?.RemoveAction(UpdateXp);
    }

    void UpdateXp() {
        xp++;
        expText.value = xp / limit;
        if (xp >= limit)
        {
            _level++;
            limit += 15;
            xp = 0;
            _shopActivatedEventHandler?.CallAction(!_shopTab.activeSelf);
            _leveledUpEventHandler?.CallAction(_level);
            Time.timeScale = 0;
            _shopTab.SetActive(true);
        }
    }

    private void UpdateLevelText()
    {
        if (_levelText == null) return;

        _levelText.text = $"LEVEL: {_level}";
    }

    private void OnShopActivated(bool gonnaBeActive)
    {
        if (gonnaBeActive) return;
        expText.value = xp / limit;
    }

}
