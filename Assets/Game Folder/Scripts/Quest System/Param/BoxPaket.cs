using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxPaket : MonoBehaviour
{
    public Paper plakatPaketRef;
    public Transform paketPossisiPegang;
    Rigidbody Rigidbody;
    bool dipegang = false;
    public Quest Quest;
    int qID;

    void Start()
    {
        paketPossisiPegang = GameObject.FindGameObjectWithTag("PegangPaket").transform;
        Rigidbody = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Rigidbody.isKinematic == true)
        {
            dipegang = true;
            
        }
        else
        {
            dipegang = false;
            CekTempat();
        }

        ShowWp();
    }

    protected void ShowWp()
    {
        float dist = Vector3.Distance(transform.position, Quest.Position.position);
        if (dist < 12)
        {
            Quest.Waypoints.SetActive(true);
        } else Quest.Waypoints.SetActive(false);
    }

    protected void CekWaktu()
    {
        if(Quest.TimeBeforeDeleted < 0) QuestManager.Instance.Cancel(Quest.QuestID);
    }

    protected void CekTempat()
    {
        float dist = Vector3.Distance(transform.position, Quest.Position.position);
        if (dist < 2)
        {
            QuestManager.Instance.FinishQuest(qID);
            Destroy(this.gameObject);
        }
    }

    public void Pegang()
    {
        if (dipegang == false)
        {
            transform.position = paketPossisiPegang.position + Vector3.up / 4;
            transform.parent = paketPossisiPegang;
            transform.rotation = Quaternion.identity * Quaternion.Euler(1,0,1);
            //transform.LookAt(paketPossisiPegang.position + Vector3.back);
            Rigidbody.isKinematic = true;
        } else
        {
            transform.parent = null;
            Rigidbody.isKinematic= false;
        }
    }

    public void SetQuest(Quest q)
    {
        Quest = q;
        qID = q.QuestID;
    }
}
