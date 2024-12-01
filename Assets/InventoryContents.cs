using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryContents : MonoBehaviour
{
    [SerializeField] Button Buttons;
    private void Start()
    {
        Buttons.onClick.AddListener(() => VehicleInventory.Instance.Ambil(this));
    }
    // Start is called before the first frame update

    public GameObject GameObject;
    public BoxPaket BoxPaket;
    public TextMeshProUGUI NamaPaket;
}
