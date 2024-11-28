using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Nama Depan Parameter", menuName = "Procedural generator / Nama Depan", order = 2)]
public class NamaDepan : ScriptableObject
{
    public List<string> Name;

    public string GetRandomName()
    {
        return Name[Random.Range(0, Name.Count - 1)];
    }
}