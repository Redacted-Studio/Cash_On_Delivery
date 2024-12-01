using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EconomyManager : MonoBehaviour
{
    static EconomyManager _instance;
    [SerializeField] float PlayerMoney = 100000;
    [SerializeField] string Currency = "IDR";

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

    public void BeliBarang(float Harga)
    {

    }

    public void DapatUang(float Jumlah)
    {

    }
}
