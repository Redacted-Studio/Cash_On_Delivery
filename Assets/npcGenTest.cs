using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcGenTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        NPCManager.Instance.GenerateNPC(Gender.Laki);
    }
}
