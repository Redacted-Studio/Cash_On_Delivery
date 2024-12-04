using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public enum ItemType
{
    MAKANAN,
    MINUMAN
}


public class Bahari : MonoBehaviour
{
    public GameObject BahariUI;
    public Item[] ItemType;

    public void BukaShop()
    {
        BahariUI.SetActive(true);
        PlayerToVehicle.Instance.playerStates = PlayerStateMovement.BELANJA;
    }

    public void Makan(string value)
    {
        for (int i = 0; i < ItemType.Length; i++)
        {
            if (ItemType[i].namaItem == value)
            {
                if (EconomyManager.Instance.CanBeliBarang(ItemType[i].Harga) == true)
                {
                    EconomyManager.Instance.BeliBarang(ItemType[i].Harga);
                    QuestManager.Instance.ShowNotification("Buy", ItemType[i].namaItem);
                    PlayerToVehicle.Instance.Makan(ItemType[i].Value);
                }
                else
                {
                    QuestManager.Instance.ShowNotification("Buy", "Not Enough Money");
                }
            }
        }
    }

    public void Minum(string value)
    {
        for (int i = 0; i < ItemType.Length; i++)
        {
            if (ItemType[i].namaItem == value)
            {
                if (EconomyManager.Instance.CanBeliBarang(ItemType[i].Harga) == true)
                {
                    EconomyManager.Instance.BeliBarang(ItemType[i].Harga);
                    QuestManager.Instance.ShowNotification("Buy", ItemType[i].namaItem);
                    PlayerToVehicle.Instance.Minum(ItemType[i].Value);
                }
                else
                {
                    QuestManager.Instance.ShowNotification("Buy", "Not Enough Money");
                }
            }
        }
    }
}

[System.Serializable]
public class Item
{
    public string namaItem;
    public ItemType type;
    public float Harga;
    public float Value;
}