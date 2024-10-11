using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pedestarian Parameter", menuName = "Procedural generator / Pedestarian Parameter", order = 0)]
public class PedestarianParameters : BaseGeneratorParam
{
    public List<Profile> ProfileNPC;
    public List<Needs> NeedsPreset;
    public List<nameValue> NameList;
}

public enum PersonalityNPC
{
    NORMAL,
    ANGER_ISSUE,
    CHILL,
    IDIOT
}


[Serializable]
public class Profile
{
    [SerializeField] protected int UID;
    public string name;
    public int Age;
    public PersonalityNPC npc;
}

[Serializable]
public class Needs
{
    [SerializeField][Range(0,100)] private int Idling;
    [SerializeField][Range(0, 100)] private int Destination;
    [SerializeField][Range(0, 100)] private int Intrupting;
}

[Serializable]
public class nameValue
{
    public string name;
}