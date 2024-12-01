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
    public Transform parentUIHP;
    public GameObject HPUIPref;

    public Transform SpawnPlace;

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
        if (tempat.GetOwner().MesenPaket == true) return;
        Quest paket = new Quest();
        paket.QuestID = quests.Count + 1;
        paket.Alamat = tempat.GetAlamat();
        paket.Penerima = tempat.GetOwner().Nama;
        paket.Position = tempat.tempatNerimaPaket;
        paket.NomorTempat = tempat.GetBuildingNumber();
        paket.NamaPaket = "Paket " + tempat.GetOwner().Nama;
        tempat.GetOwner().MesenPaket = true;
        quests.Add(paket);
        GameObject Invoice = Instantiate(Papers[UnityEngine.Random.Range(0, Papers.Count - 1)], SpawnPlace.position, Quaternion.identity);
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
                for (int j = 0; j < parentUIHP.childCount; j++)
                {
                    OrderanUI uii = parentUIHP.GetChild(j).GetComponent<OrderanUI>();
                    if (uii.QuestID == ID)
                    {
                        Destroy(uii.gameObject);
                    }
                }
            }
        }
    }

    public void NerimaQuest(int ID)
    {
        Quest questss = null;

        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i].QuestID == ID)
            {
                if (quests[i].Accepted == true) return;
                questss = quests[i];                
            }
        }

        questss.Accepted = true;
        GameObject ist = Instantiate(HPUIPref, parentUIHP);
        OrderanUI uis = ist.GetComponent<OrderanUI>();
        uis.SetUI(questss.Penerima, questss.Alamat + " No." + questss.NomorTempat.ToString(), ID);
    }

    public void NerimaRandomQuest()
    {
        int range = UnityEngine.Random.Range(1, quests.Count + 1);
        NerimaQuest(range);
    }
    
}

[Serializable]
public class Quest
{
    public int QuestID;
    public string NamaPaket;
    public string Alamat;
    public int NomorTempat;
    public string Penerima;
    public bool isFragile = false;
    public bool Accepted;
    public Transform Position;
}