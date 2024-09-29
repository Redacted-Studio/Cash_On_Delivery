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

    PedestarianDestinationType getDestinationType()
    {
        return destinationType;
    }
}
