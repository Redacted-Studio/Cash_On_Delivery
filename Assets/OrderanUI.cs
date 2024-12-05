using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OrderanUI : MonoBehaviour
{
    public TextMeshProUGUI Nama;
    public TextMeshProUGUI Alamat;
    public TextMeshProUGUI Deadline;
    public int QuestID;
    // Start is called before the first frame update
    
    public void SetUI(string name, string alamats, int Id, float deadline)
    {
        Nama.text = "Nama : " + name;
        Alamat.text = "Alamat : " + alamats;
        Deadline.text = "Sisa Waktu : " + deadline.ToString() + " Jam";
        QuestID = Id;
    }
}
