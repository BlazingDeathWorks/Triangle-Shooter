using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] private ActionChannel _leveledUpEventHandler;
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
    }

    void UpdateXp() {
        xp++;
        expText.value = xp / limit;
        if (xp >= limit)
        {
            _level++;
            limit += 20;
            xp = 0;
            _shopActivatedEventHandler?.CallAction(!_shopTab.activeSelf);
            _leveledUpEventHandler?.CallAction();
            Time.timeScale = 0;
            _shopTab.SetActive(true);
        }
    }

    private void OnShopActivated(bool gonnaBeActive)
    {
        if (gonnaBeActive) return;
        expText.value = xp / limit;
    }

}
