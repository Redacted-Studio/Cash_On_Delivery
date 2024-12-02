using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    static EconomyManager _instance;
    [SerializeField] float PlayerMoney = 100000;
    [SerializeField] string Currency = "IDR";
    public TextMeshProUGUI PhoneUI;

    private void Awake()
    {
        _instance = this;
    }

    public static EconomyManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void LateUpdate()
    {
        PhoneUI.text = string.Format("{0:n}", PlayerMoney);
    }

    public void BeliBarang(float Harga)
    {
        PlayerMoney -= Harga;
    }

    public void DapatUang(float Jumlah)
    {
        PlayerMoney += Jumlah;
    }
}
