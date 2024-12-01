using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class VehicleInventory : MonoBehaviour
{
    static VehicleInventory _instance;
    public int MaxInventory = 5;
    [SerializeField] private List<GameObject> Inventory;
    public Transform paketPossisiPegang;
    NgecekInventory Invs;
    public GameObject inventoryPrefab;
    public Transform invUIParent;

    private void Awake()
    {
        _instance = this;
    }

    public static VehicleInventory Instance
    {
        get
        {
            return _instance;
        }
    }

    void Start()
    {
        paketPossisiPegang = GameObject.FindGameObjectWithTag("PegangPaket").transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Collision");
        if (other.gameObject.CompareTag("Paket"))
        {
            Debug.Log("Paket Masuk");
            Inventory.Add(other.gameObject);
            InventorySyncronize(other.gameObject);
        }
    }

    public void InventorySyncronize(GameObject sther)
    {
            BoxPaket paketQ = sther.GetComponent<BoxPaket>();
            GameObject invent = Instantiate(inventoryPrefab, invUIParent);
            InventoryContents ins = invent.GetComponent<InventoryContents>();
            ins.BoxPaket = paketQ;
            ins.GameObject = paketQ.gameObject;
            ins.NamaPaket.text = paketQ.Quest.NamaPaket;
            sther.gameObject.SetActive(false);
        
    }

    
    public void Ambil(InventoryContents content)
    {
        GameObject contents = content.GameObject;
        contents.transform.position = paketPossisiPegang.position;
        content.BoxPaket.Pegang();
        contents.SetActive(true);
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i] == contents.gameObject)
            {
                Inventory.RemoveAt(i);
                Destroy(content.gameObject);
            }
        }
    }
}

[Serializable]
public class InventoryContent
{
    public TextMeshProUGUI UiText;
    public Button button;
    public bool diIsi = false;
}
