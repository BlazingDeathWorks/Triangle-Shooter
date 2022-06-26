using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] Slider expText = null;
    [SerializeField] float xp = 0;
    [SerializeField] float limit = 10;
    [SerializeField] ActionChannel giveXp;
    [SerializeField] private ActionChannel_Bool _shopActivatedEventHandler;
    [SerializeField] private GameObject _shopTab;
    [SerializeField] private InputButton _shopInput;
    private void Awake()
    {
        expText.value = xp/limit;
        giveXp.AddAction(UpdateXp);
    }
    void UpdateXp() {
        expText.value = xp / limit;
        if (xp < limit) { xp++; }
        else {
            limit += 20;
            xp = 0;
            _shopActivatedEventHandler?.CallAction(!_shopTab.activeSelf);
            Time.timeScale = 0;
            _shopTab.SetActive(true); 
        }
    }

}
