using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEditor;
using UnityEngine;

public enum Gender
{
    Laki,
    Perempuan
}

public class NPCManager : MonoBehaviour
{
    [Header("Generator")]
    [SerializeField] NamaDepan NamadepanGenLaki;
    [SerializeField] NamaBelakang NamaBelakangGenLaki;

    [SerializeField] NamaDepan NamadepanGenPerempuan;
    [SerializeField] NamaBelakang NamaBelakangGenPerempuan;

    [Header("Attribute")]
    public List<NPC> NPCList = new List<NPC>();

    public static NPCManager _Instance;

    private void Awake()
    {
        _Instance = this;
    }

    public static NPCManager Instance
    {
        get
        {
            return _Instance;
        }
    }

    public void GenerateNPC(Gender gender, int number)
    {
        for (int i = 0; i < number; i++)
        {
            string Nama;
            if (gender == Gender.Laki)
                Nama = NamadepanGenLaki.GetRandomName() + " " + NamaBelakangGenLaki.GetRandomName();
            else
                Nama = NamadepanGenPerempuan.GetRandomName() + " " + NamaBelakangGenPerempuan.GetRandomName();
            NPC npcs = new(Nama, UnityEngine.Random.Range(14, 60), gender);
            NPCList.Add(npcs);
        }
    }

    private void Start()
    {
        for (int i = 0;i < BuildingManager.Instance.GetBuildingCount();i++)
        {
            var randomNumber = UnityEngine.Random.Range(1, 3);
            GenerateNPC(Gender.Laki, randomNumber);
            GenerateNPC(Gender.Perempuan, randomNumber);
        }
    }

    public NPC GetRandomNPC()
    {
        return NPCList[UnityEngine.Random.Range(0, NPCList.Count - 1)];
    }

    public NPC GetHomelessNPC()
    {
        List<NPC> homeless = new List<NPC>();
        if (NPCList.Count == 0)
        {
            GenerateNPC(Gender.Laki, 3);
            GenerateNPC(Gender.Perempuan, 3);
        }

        foreach (NPC npc in NPCList)
        {
            if (npc.OwnBuilding == false && npc.Umur >= 25)
            {
                homeless.Add(npc);
            }
        }

        if (homeless.Count < 1)
        {
            var randomizer = UnityEngine.Random.Range(0, 1);
            if (randomizer == 1) GenerateNPC(Gender.Laki, 5);
            else GenerateNPC(Gender.Perempuan, 5);
        }

        return homeless[UnityEngine.Random.Range(0, homeless.Count - 1)];
    }
}

[Serializable]
public class NPC
{
    public string Nama;
    public int Umur;
    public Gender Gender;
    public bool OwnBuilding;
    public bool isRoaming= false;
    public bool MesenPaket = false;
    public Building Rumah;
    [TextArea] public string shortProfile; // For GPT Prompting

    public NPC(string Namas, int Umurs, Gender gender)
    {
        Nama = Namas;
        Umur = Umurs;
        Gender = gender;
        OwnBuilding = false;
    }
}