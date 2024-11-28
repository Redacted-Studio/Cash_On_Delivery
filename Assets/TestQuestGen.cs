using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestQuestGen : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        QuestManager.Instance.GenerateQuest();
    }
}

