using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nama Belakang Parameter", menuName = "Procedural generator / Nama Belakang", order = 3)]
public class NamaBelakang : ScriptableObject
{
    public List<string> Name;

    public string GetRandomName()
    {
        return Name[Random.Range(0, Name.Count - 1)];
    }
}