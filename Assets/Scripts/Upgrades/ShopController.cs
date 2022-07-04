using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

internal class ShopController : MonoBehaviour
{
    [SerializeField] private ActionChannel_Bool _shopActivatedEventHandler;
    [SerializeField] private Power[] _powers;
    [SerializeField] private Text _descriptionText;
    [SerializeField] private int _availableSlots = 3;
    private List<GameObject> _activePowers = new List<GameObject>();
    private Power _currentPower;

    private void Awake()
    {
        foreach (Power power in _powers)
        {
            power.gameObject.SetActive(false);
        }
    }

    private void OnEnable()
    {
        List<int> usedNumbers = new List<int>(_availableSlots);
        for (int i = 0; i < _availableSlots; i++)
        {
            int randomNum = 0;

            do
            {
                randomNum = Random.Range(0, _powers.Length);
            } while (usedNumbers.Contains(randomNum));

            usedNumbers.Add(randomNum);
            GameObject power = _powers[randomNum].gameObject;
            power.SetActive(true);
            _activePowers.Add(power);
        }
    }

    private void ClearActivePowers()
    {
        foreach (GameObject power in _activePowers)
        {
            power.SetActive(false);
        }
        _activePowers.Clear();
    }

    public void UpdatePower(Power power)
    {
        _descriptionText.text = $"{power.PowerData.Name}\n\n";
        _descriptionText.text += power.PowerData.Description;
        _currentPower = power;
    }

    public void SelectPower()
    {
        if (_currentPower == null) return;

        _descriptionText.text = "";
        _currentPower.Upgrade();
        _shopActivatedEventHandler?.CallAction(!gameObject.activeSelf);
        Time.timeScale = 1;
        _currentPower = null;
        ClearActivePowers();
        gameObject.SetActive(false);
    }
}
