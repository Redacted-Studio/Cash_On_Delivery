using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static QuestManager _Instance;
    public TextMeshProUGUI textMeshProUGUI;

    [Header("QUESTING LIST")]
    public List<Quest> quests;
    private void Awake()
    {
        _Instance = this;
    }

    public static QuestManager Instance
    {
        get
        {
            return _Instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateQuest()
    {
        Building tempat = BuildingManager.Instance.GetRandomBuilding();
        Quest paket = new Quest();
        paket.Alamat = tempat.GetAlamat();
        paket.Penerima = tempat.GetOwner().Nama;
        paket.Position = tempat.tempatNerimaPaket;
        quests.Add(paket);
    }
}

[Serializable]
public class Quest
{
    public string NamaPaket;
    public string Alamat;
    public string Penerima;
    public bool Accepted;
    public Transform Position;
}