using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] Slider expText = null;
    [SerializeField] float xp = 0;
    [SerializeField] float limit = 100;
    [SerializeField] ActionChannel giveXp;
    private void Awake()
    {
        expText.value = xp/limit;
        giveXp.AddAction(UpdateXp);
    }
    void UpdateXp() {
        expText.value = xp/limit;
        if (xp < limit) xp++;
    }

}
