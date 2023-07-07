using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator anim;
    const string GAME_START = "Start";
    const string JUMP = "Jump";
    const string DEATH = "Died";

    private void Awake()
    {
        anim = this.GetComponent<Animator>();
    }

    private void Update()
    {
        if (anim)
        {
            anim.SetBool(GAME_START, Player.instance.GameStart());
            anim.SetBool(JUMP, Player.instance.Jumping());
            if (!Player.instance.PlayerAlive())
                anim.SetTrigger(DEATH);
        }
    }
}
