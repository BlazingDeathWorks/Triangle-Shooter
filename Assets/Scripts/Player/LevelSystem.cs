using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelSystem : MonoBehaviour
{
    [SerializeField] Text expText = null;
    [SerializeField] int xp = 0;
    [SerializeField] int limit = 100;
    [SerializeField] ActionChannel giveXp;
    private void Awake()
    {
        expText.text = "EXP:" + xp + "/" + limit;
        giveXp.AddAction(UpdateXp);
    }
    void UpdateXp() {
        expText.text = "EXP:" + xp + "/" + limit;
        if (xp < limit) xp++;
    }

}
