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

    [Header("QUESTING LIST")]
    public List<Quest> quests;

    [Header("PAPER PREFAB")]
    public List<GameObject> Papers;
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
        paket.QuestID = quests.Count + 1;
        paket.Alamat = tempat.GetAlamat();
        paket.Penerima = tempat.GetOwner().Nama;
        paket.Position = tempat.tempatNerimaPaket;
        quests.Add(paket);
        GameObject Invoice = Instantiate(Papers[UnityEngine.Random.Range(0, Papers.Count - 1)]);
        Paper pap = Invoice.GetComponent<Paper>();
        pap.Set(paket.Alamat, paket.Penerima, paket.isFragile);
        pap.transform.parent = QuestPool.transform;
        pap.SetQuest(paket.QuestID);
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