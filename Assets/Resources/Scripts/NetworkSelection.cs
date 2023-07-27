using Unity.Netcode;

public class NetworkSelection : NetworkBehaviour
{
    public void OnClickHost()
    {
        NetworkManager.Singleton.StartHost();
    }

    public void OnClickClient()
    {
        NetworkManager.Singleton.StartClient();
    }
}
