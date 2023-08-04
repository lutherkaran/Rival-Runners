using Unity.Netcode;
using UnityEngine;

public class NetworkSelection : NetworkBehaviour
{
    [SerializeField] private TMPro.TextMeshProUGUI players;

    private NetworkVariable<int> playersNumber = new NetworkVariable<int>(0, NetworkVariableReadPermission.Everyone);

    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void OnClickClient()
    {
        NetworkManager.Singleton.StartClient();
    }

    private void Update()
    {
        players.text = "Players: " + playersNumber.Value;
        if (!IsOwner) return;
        playersNumber.Value = NetworkManager.Singleton.ConnectedClients.Count;
    }
}
