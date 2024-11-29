using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PedestarianDestinationType
{
    ROAMING,
    SHOP,
    UNDEFINED
}

public class PedestarianDestination : DestinationBase
{
    [SerializeField] PedestarianDestinationType destinationType;

    public PedestarianDestinationType getDestinationType()
    {
        return destinationType;
    }

    private void Start()
    {
        var AIManager = FindObjectOfType<AIManager>();
        if (AIManager)
            AIManager.RegisterDestination(this);
    }
}
