using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    Vector3 offset = new Vector3(0, 0.7f, -2f);
    Vector3 offset2 = new Vector3(-0.09f, 3.53f, -1.99f);
    [SerializeField] Transform playerTarget;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        playerTarget = PlayerController.Instance.transform;
    }

    private void LateUpdate()
    {
        if (playerTarget)
        {
            this.transform.position = playerTarget.position + offset2;
        }
    }
}
