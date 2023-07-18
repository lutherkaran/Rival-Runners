using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;

    const string GAME_START = "Start";
    const string JUMP = "Jump";
    const string DEATH = "Died";

    bool playerAlive = true;

    private void OnEnable()
    {
        Player.OnDied += PlayerAlive;
    }

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim)
        {
            anim.SetBool(GAME_START, Player.Instance.GameStart());
            anim.SetBool(JUMP, Player.Instance.Jumping());
            if (!playerAlive)
                anim.SetTrigger(DEATH);
        }
    }

    void PlayerAlive()
    {
        playerAlive = false;
    }

    private void OnDisable()
    {
        Player.OnDied -= PlayerAlive;
    }
}
