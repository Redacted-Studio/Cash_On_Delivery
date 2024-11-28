using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    static AIManager instance;
    private NetworkVariable<int> aiNumber = new NetworkVariable<int>();
    private List<AIBase> AIRegistered = new List<AIBase>();
    public List<PedestarianDestination> PedestarianDestinations;

    private void Start()
    {
        if (instance != null && instance != this) Destroy(this);
        else if (instance == null) instance = this;
    }

    #region Static Function

    public static AIManager GetInstances()
    {
        return instance;
    }

    #endregion

    #region Public Function

    /// <summary>
    /// Register AI ke AI Manager
    /// </summary>
    /// <param name="reg"> AIBase Param </param>
    public void RegisterAI(AIBase reg)
    {
        AIRegistered.Add(reg);
        aiNumber.Value = AIRegistered.Count;
        Debug.Log(aiNumber.Value);
    }

    /// <summary>
    /// Un Register AI
    /// </summary>
    /// <param name="reg"> Harus Inherit dari AIBase </param>
    public void UnregisterAI(AIBase reg)
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

        AIRegistered.Remove(reg);
        aiNumber.Value = AIRegistered.Count;
        Debug.Log(aiNumber.Value);

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
}
