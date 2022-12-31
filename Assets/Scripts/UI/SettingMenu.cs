using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Audio;
using TMPro; 

public class SettingMenu : MonoBehaviour
{
    [Header("Volume Settings")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private Slider volumeSilder = null;

    [SerializeField] private GameObject applyButton;

    private void Start()
    {
        AudioListener.volume = PlayerPrefs.GetFloat("volume");
        volumeSilder.value = PlayerPrefs.GetFloat("volume");
        volumeTextValue.text = volumeSilder.value.ToString("0.0");

    }

    public void SetVolume() {
        AudioListener.volume = volumeSilder.value;
        volumeTextValue.text = volumeSilder.value.ToString("0.0");
    }

    public void VolumeApply() {
        PlayerPrefs.SetFloat("volume", AudioListener.volume);
        StartCoroutine(DelayButton());
    }

    public IEnumerator DelayButton() {
        applyButton.SetActive(false);
        yield return new WaitForSeconds(2);
        applyButton.SetActive(true);
    }

}
