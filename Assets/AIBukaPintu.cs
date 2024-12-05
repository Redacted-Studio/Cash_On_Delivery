using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class AIBukaPintu : MonoBehaviour
{
    public Animator animator;
    public LayerMask m_LayerMask;
    public void BukaPintu()
    {
        if (animator == null) return;
        animator.SetBool("Terbukalah", true);
    }

    private void LateUpdate()
    {
        OverlapCheck();
    }
    void OverlapCheck()
    {
        Collider[] hitColliders = Physics.OverlapBox(gameObject.transform.position, transform.localScale / 2, Quaternion.identity, m_LayerMask);
        int i = 0;
        while (i < hitColliders.Length)
        {
            BukaPintu();
            //Debug.Log("Hit : " + hitColliders[i].name + i);
            i++;
        }
        if(animator)
        {
            if (hitColliders.Length < 1)
                animator.SetBool("Terbukalah", false);
        }
    }
}
