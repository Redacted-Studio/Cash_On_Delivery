using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    static AIManager _instance;
    public List<NormalPedestarian> RegAI;
    public List<PedestarianDestination> PedestarianDestinations;
    public List<GameObject> AIPrefab;
    public Transform pool;

    public int MaxAILimit = 20;

    private void Awake()
    {
        _instance = this;
    }

    public static AIManager Instance
    {
        get
        {
            return _instance;
        }
    }

    private void Start()
    {
        StartCoroutine(spawnRoutine());
    }

    #region Public Function

    /// <summary>
    /// Register AI ke AI Manager
    /// </summary>
    /// <param name="reg"> AIBase Param </param>
    public void RegisterAI(NormalPedestarian reg)
    {
        RegAI.Add(reg);
    }

    /// <summary>
    /// Un Register AI
    /// </summary>
    /// <param name="reg"> Harus Inherit dari AIBase </param>
    public void UnregisterAI(NormalPedestarian reg)
    {
        /*for (int i = AIRegistered.Count - 1; i >= 0; i--)
        {
            if(AIRegistered[i] == reg)
            {
                AIRegistered.RemoveAt(i);
                aiNumber.Value = AIRegistered.Count;
                Debug.Log(aiNumber.Value);
                break;
            }
        }*/
        reg.profile.isRoaming = false;
        RegAI.Remove(reg);

        /*foreach(var  ai in AIRegistered)
        {
            if (ai == null)
            {
                AIRegistered.Remove(ai);
                aiNumber.Value = AIRegistered.Count;
                Debug.Log(aiNumber.Value);
            }
        }*/
    }

    public void RegisterDestination(PedestarianDestination destination)
    {
        PedestarianDestinations.Add(destination);
    }

    #endregion

    private void LateUpdate()
    {
        if (RegAI.Count < MaxAILimit)
            StartCoroutine(spawnRoutine());

        foreach (NormalPedestarian ai in RegAI)
        {
            if (ai.isHavingDestination == false)
                ai.SetDestination(GetRandomDestination());
        }
    }
    IEnumerator spawnRoutine()
    {
        while (true)
        {
            if (RegAI.Count > MaxAILimit) break;
            SpawnAIFun();
            yield return new WaitForSeconds(1);
        }
    }

    protected void SpawnAIFun()
    {
        //if (RegAI.Count > MaxAILimit) return;

        PedestarianDestination spawn = GetRandomDestination();
        GameObject ped = Instantiate(AIPrefab[Random.Range(0, AIPrefab.Count - 1)], spawn.transform);
        ped.transform.parent = pool;
        NormalPedestarian norm = ped.GetComponent<NormalPedestarian>();
        RegisterAI(norm);
        norm.RegisterNPCProfile(NPCManager.Instance.GetRandomNPC());
        norm.profile.isRoaming = true;
        norm.isHavingDestination = true;
        norm.SetDestination(GetRandomDestination());
    }

    public void SetAILimit(int Limit)
    {
        MaxAILimit = Limit;
    }

    public PedestarianDestination GetRandomDestination()
    {
        return PedestarianDestinations[UnityEngine.Random.Range(0, PedestarianDestinations.Count - 1)];
    }
}
