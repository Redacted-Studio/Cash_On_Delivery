using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Paper : MonoBehaviour
{
    public TextMeshProUGUI Alamat;
    public TextMeshProUGUI Penerima;
    public Image FragileImg;
    bool isFragiles;

    public void Set(string alamat, string penerima, bool isFragile = false)
    {
        Alamat.text = "Alamat : " + alamat;
        Penerima.text = "Penerima : " + penerima;
        isFragiles = isFragile;
        if (isFragile) FragileImg.gameObject.SetActive(true);
    }
}
