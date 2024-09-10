using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


[RequireComponent(typeof(NavMeshAgent))]
public abstract class AIBase : MonoBehaviour
{
    virtual public GameObject getParentGameObject()
    {
        if (this.gameObject.GetComponentInParent<GameObject>() != null)
            return this.gameObject.GetComponentInParent<GameObject>();

        return this.gameObject;
    }

    virtual public Transform getOrigin() { return this.transform; }
    virtual public NavMeshAgent getNavmeshAgent() { return this.getNavmeshAgent(); }
}