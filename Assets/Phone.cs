using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject HomePage;
    public GameObject GPSPage;
    public GameObject ApplicationPage;
    public GameObject BankPage;
    public GameObject MusicPage;

    public void GoToGPS()
    {
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(true);
    }

    public void GoHome()
    {
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(true);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void GoApplication()
    {
        MusicPage.SetActive(false);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(true);
        GPSPage.SetActive(false);
    }

    public void GoBank()
    {
        MusicPage.SetActive(false);
        BankPage.SetActive(true);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }
    public void GoMusic()
    {
        MusicPage.SetActive(true);
        BankPage.SetActive(false);
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void GantiRadioStation(int rad)
    {
        AudioManager.Instance.GantiChannel(rad);
    }
}
