using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderanUI : MonoBehaviour
{
    public TextMeshProUGUI Nama;
    public TextMeshProUGUI Alamat;
    public int QuestID;
    // Start is called before the first frame update
    
    public void SetUI(string name, string alamats, int Id)
    {
        Nama.text = "Nama : " + name;
        Alamat.text = "Alamat : " + alamats;
        QuestID = Id;
    }
}
