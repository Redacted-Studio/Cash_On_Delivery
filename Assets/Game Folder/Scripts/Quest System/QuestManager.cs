using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    static QuestManager _Instance;
    public TextMeshProUGUI textMeshProUGUI;
    public GameObject QuestPool;
    public Transform ObjectPool;
    public int FinishedQuest;

    [Header("QUESTING LIST")]
    public List<Quest> quests;

    [Header("PAPER PREFAB")]
    public List<GameObject> Papers;
    private void Awake()
    {
        _Instance = this;
    }

    private void Start()
    {
        textMeshProUGUI.text = "Quest Available : " + quests.Count;
    }

    public static QuestManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    public void GenerateQuest()
    {
        Building tempat = BuildingManager.Instance.GetRandomBuilding();
        Quest paket = new Quest();
        paket.QuestID = quests.Count + 1;
        paket.Alamat = tempat.GetAlamat();
        paket.Penerima = tempat.GetOwner().Nama;
        paket.Position = tempat.tempatNerimaPaket;
        paket.NamaPaket = "Paket " + tempat.GetOwner().Nama;
        quests.Add(paket);
        GameObject Invoice = Instantiate(Papers[UnityEngine.Random.Range(0, Papers.Count - 1)]);
        Invoice.transform.parent = QuestPool.transform;
        BoxPaket paps = Invoice.GetComponent<BoxPaket>();
        paps.SetQuest(paket);
        Paper pap = paps.plakatPaketRef;
        pap.Set(paket.Alamat, paket.Penerima, paket.isFragile);
        textMeshProUGUI.text = "Quest Available : " + quests.Count;
        //pap.transform.parent = QuestPool.transform;
    }

    public void FinishQuest(int ID)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].QuestID == ID)
            {
                quests.RemoveAt(i);
                FinishedQuest++;
                textMeshProUGUI.text = "Quest Available : " + quests.Count;
            }
        }
    }

    public void NerimaQuest(int ID)
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].QuestID == ID)
            {
                quests[i].Accepted = true;
            }
        }
    }
    
}

[Serializable]
public class Quest
{
    public int QuestID;
    public string NamaPaket;
    public string Alamat;
    public string Penerima;
    public bool isFragile = false;
    public bool Accepted;
    public Transform Position;
}