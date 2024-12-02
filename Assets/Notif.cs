using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Notif : MonoBehaviour
{
    public TextMeshProUGUI header;
    public TextMeshProUGUI msg;

    public void SetNotif(string head, string msgg)
    {
        header.text = head;
        msg.text = msgg;
    }

    private void Start()
    {
        Destroy(this.gameObject, 5f);
    }
}
