using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimController : Singleton<PlayerAnimController>
{
    public Animator playerAnimation;

    void Start()
    {
        playerAnimation = GetComponent<Animator>(); 
    }

    void IdleToWalk()
    {
        playerAnimation.SetBool("Idle-Walk",true);
    }

    public void WalkToSlap()
    {
        playerAnimation.SetBool("Walk-Slap", true);
    }

    public void SlapToRunWithDonuts()
    {
        playerAnimation.SetBool("Slap-RunDonut", true);

    }

    public void RunWithDonutsToFALL()
    {

        playerAnimation.SetBool("RunDonut-Fall", true);
    }


    private void OnEnable()
    {
        GameManager.OnGameStart += IdleToWalk;
        //GameManager.OnGameWin += Win;
        //GameManager.OnGameLose += Lose;
    }

    private void OnDisable()
    {
        // GameManager.OnGameStart -= CharacterMove;
        // GameManager.OnGameWin -= Win;
        //  GameManager.OnGameLose -= Lose;
    }

}
