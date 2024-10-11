using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.Netcode;
using Unity.Netcode.Transports.UTP;
using UnityEngine;
using UnityEngine.UI;

public class LobbyHandler : MonoBehaviour
{
    [Header("Component Refrences")]
    [SerializeField] UnityTransport transport;
    [SerializeField] NetworkManager networkManager;

    public static LobbyHandler singleton;

    [SerializeField] Button ServerButt;
    [SerializeField] Button HostButt;
    [SerializeField] Button ClientButt;
    [SerializeField] Button StopButt;

    [Header("Player Refrences")]

    [SerializeField] TMP_InputField IPConfiguration;
    [SerializeField] TMP_InputField playerName;

    [SerializeField] List<Transform> playerSpawnPosition;

    string LocalPlayerName;

    // Start is called before the first frame update
    private void Awake()
    {
        ServerButt.onClick.AddListener(Starthandler);
        HostButt.onClick.AddListener(Hosthandler);
        ClientButt.onClick.AddListener(Clientthandler);
        StopButt.onClick.AddListener(Disconnecthandler);
        IPConfiguration.onValueChanged.AddListener(validateIPAddress);
        playerName.onValueChanged.AddListener(setPlayerName);
        ClientButt.interactable = false;

        DontDestroyOnLoad(this);

        if (singleton != null && singleton != this)
        {
            Destroy(gameObject);
        } else
        {
            singleton = this;
        }

        DontDestroyOnLoad(this);
    }
    public void setIP(string IP) => transport.ConnectionData.Address = IP;

    void validateIPAddress(string text)
    {
        if (ValidateIPv4(text))
        {
            ClientButt.interactable = true;
            setIP(text);
        } else ClientButt.interactable = false;
    }

    public string getPLayerName()
    {
        return LocalPlayerName;
    }

    void setPlayerName(string name)
    {
        LocalPlayerName = name;
    }

    public bool ValidateIPv4(string ipString)
    {
        if (String.IsNullOrEmpty(ipString))
        {
            return false;
        }

        string[] splitValues = ipString.Split('.');
        if (splitValues.Length != 4)
        {
            return false;
        }

        byte tempForParsing;

        return splitValues.All(r => byte.TryParse(r, out tempForParsing));
    }

    private void Update()
    {
        //Debug.Log(IPConfiguration.text);
    }

    void Starthandler()
    {
        NetworkManager.Singleton.StartServer();
        IPConfiguration.gameObject.SetActive(false);
        playerName.gameObject.SetActive(false);
        ServerButt.interactable = false;
        HostButt.interactable = false;
        ClientButt.interactable = false;
        StopButt.interactable = true;
    }
    void Hosthandler()
    {
        NetworkManager.Singleton.StartHost();
        IPConfiguration.gameObject.SetActive(false);
        playerName.gameObject.SetActive(false);
        ServerButt.interactable = false;
        HostButt.interactable = false;
        ClientButt.interactable = false;
        StopButt.interactable = true;
    }
    void Clientthandler()
    {
        NetworkManager.Singleton.StartClient();
        IPConfiguration.gameObject.SetActive(false);
        playerName.gameObject.SetActive(false);
        ServerButt.interactable = false;
        HostButt.interactable = false;
        ClientButt.interactable = false;
        StopButt.interactable = true;
    }
    void Disconnecthandler()
    {
        NetworkManager.Singleton.Shutdown(true);
        IPConfiguration.gameObject.SetActive(true);
        playerName.gameObject.SetActive(true);
        ServerButt.interactable = true;
        HostButt.interactable = true;
        ClientButt.interactable = true;
        StopButt.interactable = false;
    }

    public Transform getPlayerPosition(out List<Transform> playerRes)
    {
        playerRes = playerSpawnPosition;
        return null;
    }
}