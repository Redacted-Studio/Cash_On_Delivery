using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerHUD : NetworkBehaviour
{
    NetworkVariable<NetworkString> playerName = new NetworkVariable<NetworkString>("Unknown",NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] private TextMeshProUGUI myUGUI;

    [SerializeField] private GameObject obj;

    public override void OnNetworkSpawn()
    {
        if (IsOwner)
        {
            obj = GameObject.FindGameObjectWithTag("NetworkUIManager");
            var gc = obj.GetComponent<LobbyHandler>();
            playerName.Value = gc.getPLayerName();
            myUGUI.gameObject.SetActive(true);
        }

        myUGUI.text = playerName.Value.ToString();
        playerName.OnValueChanged += playerNameOnValue_Changed;
    }

    private void playerNameOnValue_Changed(NetworkString prevs, NetworkString news)
    {
        myUGUI.text = news;
    }
}

