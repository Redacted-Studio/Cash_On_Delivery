using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class VehicleInventory : MonoBehaviour
{
    public int MaxInventory = 5;
    [SerializeField] private List<GameObject> Inventory;
    public Transform paketPossisiPegang;
    NgecekInventory Invs;

    // Start is called before the first frame update
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
            InventorySyncronize();
            other.gameObject.SetActive(false);
        }
    }

    public void InventorySyncronize()
    {
        for(int i = 0; i < Inventory.Count; i++)
        {
            BoxPaket paketQ = Inventory[i].GetComponent<BoxPaket>();
            Invs.InventoryContent[i].UiText.text = paketQ.Quest.NamaPaket;
            Invs.InventoryContent[i].UiText.gameObject.SetActive(true);
            Invs.InventoryContent[i].button.gameObject.SetActive(true);
            Invs.InventoryContent[i].diIsi = true;
        }
    }

    public void AmbilBarang(int i)
    {
        Inventory[i].transform.position = paketPossisiPegang.transform.position;
        Inventory[i].gameObject.SetActive(true);
        Invs.InventoryContent[i].UiText.text = "";
        Invs.InventoryContent[i].UiText.gameObject.SetActive(false);
        Invs.InventoryContent[i].button.gameObject.SetActive(false);
        Invs.InventoryContent[i].diIsi = false;
        Inventory[i] = null;
    }

    public NgecekInventory setInvs(NgecekInventory q)
    {
        Invs = q;
        for (int i = 0; i < q.InventoryContent.Count; i++)
        {
            Invs.InventoryContent[i].UiText.gameObject.SetActive(false);
            Invs.InventoryContent[i].button.gameObject.SetActive(false);
            Invs.InventoryContent[i].diIsi = false;
        }
        return q;
    }
}

[Serializable]
public class InventoryContent
{
    public TextMeshProUGUI UiText;
    public Button button;
    public bool diIsi = false;
}
