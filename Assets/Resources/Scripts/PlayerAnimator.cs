using Unity.Netcode;
using UnityEngine;

public class PlayerAnimator : NetworkBehaviour
{
    Animator anim;

    const string GAME_START = "Start";
    const string JUMP = "Jump";
    const string DEATH = "Died";

    bool playerAlive = true;

    private void OnEnable()
    {
        PlayerController.OnDied += PlayerAlive;
    }

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (!IsHost && !IsOwner) return;
        if (GameMenuManager.Instance.timer.GetRemainingTime() == 0)
        {
            if (anim)
            {
                anim.SetBool(GAME_START, PlayerController.Instance.playerStarted);
                anim.SetBool(JUMP, PlayerController.Instance.jumping);
                if (!playerAlive)
                    anim.SetTrigger(DEATH);
            }
        }
    }

    void PlayerAlive()
    {
        playerAlive = false;
    }

    private void OnDisable()
    {
        PlayerController.OnDied -= PlayerAlive;
    }
}
