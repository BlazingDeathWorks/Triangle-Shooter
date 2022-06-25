using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class ShopController : MonoBehaviour
{
    [SerializeField] private Power[] _powers;
    [SerializeField] private int _availableSlots = 3;

    private void OnEnable()
    {
        List<int> usedNumbers = new List<int>(_availableSlots);
        for (int i = 0; i < _availableSlots; i++)
        {
            int randomNum = 0;

            do
            {
                randomNum = Random.Range(0, _powers.Length);
                Debug.Log(randomNum);
            } while (usedNumbers.Contains(randomNum));

            usedNumbers.Add(randomNum);
            _powers[randomNum].gameObject.SetActive(true);
        }
    }
}
