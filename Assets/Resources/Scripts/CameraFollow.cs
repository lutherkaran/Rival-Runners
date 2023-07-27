using Unity.Netcode;
using UnityEngine;

public class CameraFollow : NetworkBehaviour
{
    //Vector3 offset = new Vector3(0, 0.46f, -2.3f);
    Vector3 offset2 = new Vector3(0, .6f, -1.4f);
    [SerializeField] Transform playerTarget;

    public override void OnNetworkSpawn()
    {
        base.OnNetworkSpawn();
        //playerTarget = PlayerController.Instance.transform;
        playerTarget = this.GetComponentInParent<PlayerController>().transform;
    }

    private void LateUpdate()
    {
        if (playerTarget)
        {
            this.transform.position = playerTarget.position + offset2;
        }
    }
}
