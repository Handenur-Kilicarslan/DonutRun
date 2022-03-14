
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionsController : MonoBehaviour
{
    public List<GameObject> Donuts;

    private void Start()
    {
        for (int i = 0; i < Donuts.Count; i++)
        {
            Donuts[i].SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("Obstacle");
            PlayerAnimController.Instance.RunWithDonutsToFALL();
            GameManager.Instance.LoseGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out CollectableController donut))
        {
            Debug.Log("A Donut!");
            donut.gameObject.SetActive(false);
            Donuts[DonutLastControl(Donuts)].SetActive(true);
        }


        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("THIS IS POLICEMAN");
            police.GetHisDonut(transform);
            Donuts[DonutLastControl(Donuts)].SetActive(true);
            PlayerAnimController.Instance.WalkToSlap();
            PathFollower.Instance.speed += 1;
        }

        if (other.gameObject.CompareTag("End"))
        {
            Policeman.policeMoving = false;
            PathFollower.Instance.speed -= 2;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("polisten ayrýldýk");
            PlayerAnimController.Instance.SlapToRunWithDonuts();
        }
    }


    public void DistributeDonuts()
    {
        int last = DonutLastControl(Donuts);

        for(int i = 1; i<last; i++)
        {
            Debug.Log("dÝSTRUBUTE THEM TO HOMELESSES");
        }
    }
    public int DonutLastControl(List<GameObject> Donuts)
    {
        int lastIndex = 0;

        for (int i = 0; i < Donuts.Count; i++)
        {
            if(Donuts[i].activeSelf == true)
            {
                lastIndex++;
            }
        }
        return lastIndex;

        
    }


    private void OnEnable()
    {
        // GameManager.OnGameStart += CharacterMove;
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
