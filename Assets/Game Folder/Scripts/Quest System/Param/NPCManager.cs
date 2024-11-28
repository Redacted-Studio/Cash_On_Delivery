using System;
using System.Collections;
using System.Collections.Generic;
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
    public List<NPC> NPCList;

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

    public NPC GenerateNPC(Gender gender)
    {
        string Nama;
        if (gender == Gender.Laki)
            Nama = NamadepanGenLaki.GetRandomName() + " " + NamaBelakangGenLaki.GetRandomName();
        else
            Nama = NamadepanGenPerempuan.GetRandomName() + " " + NamaBelakangGenPerempuan.GetRandomName();
        NPC npcs = new(Nama, UnityEngine.Random.Range(14, 60), gender);
        NPCList.Add(npcs);
        return npcs;
    }

    public NPC GetRandomNPC()
    {
        return NPCList[UnityEngine.Random.Range(0, NPCList.Count - 1)];
    }
}

[Serializable]
public class NPC
{
    public string Nama;
    public int Umur;
    public Gender Gender;


    public NPC(string Namas, int Umurs, Gender gender)
    {
        Nama = Namas;
        Umur = Umurs;
        Gender = gender;
    }
}