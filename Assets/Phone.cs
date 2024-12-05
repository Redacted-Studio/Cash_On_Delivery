using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Phone : MonoBehaviour
{
    public GameObject HomePage;
    public GameObject GPSPage;
    public GameObject ApplicationPage;
    public GameObject BankPage;
    public GameObject MusicPage;
    public GameObject SettingPage;

    [Header("Setting Refrences")]
    // Setting Ref
    public Slider maxAISlider;
    public TextMeshProUGUI maxAIText;
    public Slider maxVehSlider;
    public TextMeshProUGUI maxVehText;

    private void Start()
    {
        Inits();
    }
    public void Exit()
    {
        Application.Quit();
    }

    protected void Inits()
    {
        maxAISlider.value = AIManager.Instance.MaxAILimit;
        maxVehSlider.value = PathFinder.Instance.maxCars;
    }

    public void GoToGPS()
    {
        SettingPage.SetActive(false);
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(true);
    }

    public void GoHome()
    {
        SettingPage.SetActive(false);
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(true);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void GoApplication()
    {
        SettingPage.SetActive(false);
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(true);
        GPSPage.SetActive(false);
    }

    public void GoBank()
    {
        SettingPage.SetActive(false);
        MusicPage.SetActive(false);
        BankPage.SetActive(true);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }
    public void GoMusic()
    {
        SettingPage.SetActive(false);
        MusicPage.SetActive(true);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void GoSetting()
    {
        SettingPage.SetActive(true);
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void SyncSliderToValMaxAI()
    {
        maxAIText.text = maxAISlider.value.ToString();
        int lim = Mathf.RoundToInt(maxAISlider.value);
        AIManager.Instance.SetAILimit(lim);
    }

    public void SyncSliderToValMaxVeh()
    {
        maxVehText.text = maxVehSlider.value.ToString();
        int lim = Mathf.RoundToInt(maxAISlider.value);
        PathFinder.Instance.SetMaxCar(lim);
    }

    public void GantiRadioStation(int rad)
    {
        AudioManager.Instance.GantiChannel(rad);
    }
}
