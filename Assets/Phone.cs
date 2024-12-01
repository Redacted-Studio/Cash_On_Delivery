using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Phone : MonoBehaviour
{
    public GameObject HomePage;
    public GameObject GPSPage;
    public GameObject ApplicationPage;

    public void GoToGPS()
    {
        HomePage.SetActive(false);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(true);
    }

    public void GoHome()
    {
        HomePage.SetActive(true);
        ApplicationPage.SetActive(false);
        GPSPage.SetActive(false);
    }

    public void GoApplication()
    {
        HomePage.SetActive(false);
        ApplicationPage.SetActive(true);
        GPSPage.SetActive(false);
    }
}
