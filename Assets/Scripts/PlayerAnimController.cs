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

    public void RunWithDonutsToWalk()
    {

        playerAnimation.SetBool("RunDonut-Fall", true);
    }
    

    public void RunDonutToRunDonutSlap()
    {
        playerAnimation.SetBool("RunDonut-SlapDonut", true);
    }
    public void RunDonutSlapExit()
    {
        playerAnimation.SetBool("RunDonut-SlapDonut", false);
    }

    public void FallWalkToWinDance()
    {
        playerAnimation.SetBool("EndWalkToWinDance", true);
    }

    public void RunWithDonutObstacle()
    {

        playerAnimation.SetBool("Idle-Walk", false);
        playerAnimation.SetBool("endIdle", true);

    }


    private void OnEnable()
    {
        GameManager.OnGameStart += IdleToWalk;
        //GameManager.OnGameWin += Win;
        //GameManager.OnGameLose += Lose;
    }

    private void OnDisable()
    {
        GameManager.OnGameStart -= IdleToWalk;
        // GameManager.OnGameWin -= Win;
        //  GameManager.OnGameLose -= Lose;
    }

}


/* void IdleToWalk()
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

 * */
