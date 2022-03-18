
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerActionsController : MonoBehaviour
{
    public List<GameObject> Donuts;
    public GameObject FallingDonuts;
    public GameObject SlapEffect;
    public DonutMoveController donutMoveController;
    public SMoveController stackMove;

    private void Start()
    {
        for (int i = 0; i < Donuts.Count; i++)
        {
            Donuts[i].SetActive(false);
        }
    }
    private void Update()
    {
        Debug.Log("Active Donuts : " + DonutLastControl(Donuts));
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("Obstacle");
            PlayerAnimController.Instance.RunWithDonutObstacle();

            DonutsFalling();
            DOVirtual.DelayedCall(.5f, () => Policeman.allPoliceMoving = false);
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

        if (other.gameObject.CompareTag("End"))
        {
            Policeman.allPoliceMoving = false;
            PlayerAnimController.Instance.RunWithDonutsToWalk();
            PathFollower.Instance.speed -= 1;
            StartCoroutine(DistributeDonutsandStopMoving());

        }

        if (other.gameObject.CompareTag("Levha"))
        {
            for (int i = 14; i < Donuts.Count; i++)
            {
                Donuts[i].SetActive(false);
                //onun transformundan donutlarý düþür ama donut varsa activeself ile kontrol edersin
            }
        }

        if (other.gameObject.TryGetComponent(out WallDown wall))
        {
            wall.MakeWallFallDown();
        }

        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("THIS IS POLICEMAN");

            police.GetHisDonut(transform);

            DOVirtual.DelayedCall(.3f, () => Donuts[DonutLastControl(Donuts)].SetActive(true));

            if (DonutLastControl(Donuts) <= 1)
            {
                PlayerAnimController.Instance.WalkToSlap();
            }
            else if (DonutLastControl(Donuts) > 1)
            {
                PlayerAnimController.Instance.RunDonutToRunDonutSlap();
            }

            StartCoroutine(SlapEffectDo(police.gameObject));
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("polisten ayrýldýk");

            if (DonutLastControl(Donuts) <= 1)
            {
                PlayerAnimController.Instance.SlapToRunWithDonuts();
            }
            else if (DonutLastControl(Donuts) > 1)
            {
                PlayerAnimController.Instance.RunDonutSlapExit();
            }

        }
    }



    public IEnumerator DistributeDonutsandStopMoving()
    {
        transform.parent.transform.DOLocalMoveX(-0.908f, .5f);
        yield return new WaitForSeconds(.5f);
        donutMoveController.enabled = false;
        stackMove.enabled = false;


        int last = DonutLastControl(Donuts);
        for (int i = last; i >= 1; i -= 2)
        {
            Debug.Log("Lets Help The Poor People!");
            Donuts[i].transform.parent = null;
            Donuts[i].transform.DOMoveX(Donuts[i].transform.position.x + 8, 1f);
            yield return new WaitForSeconds(.4f);
            Donuts[i-1].transform.parent = null;
            Donuts[i - 1].transform.DOMoveX(Donuts[i - 1].transform.position.x - 8, 1f);
        }

        if (last % 2 == 1)
        {
            Debug.Log("Tek");
            Donuts[0].transform.DOMoveX(Donuts[0].transform.position.x + 8, 1f);
        }

        yield return new WaitForSeconds(1);
       //PlayerAnimController.Instance.FallWalkToWinDance();

    }

    public int DonutLastControl(List<GameObject> Donuts)
    {
        int lastIndex = 0;
        for (int i = 0; i < Donuts.Count; i++)
        {
            if (Donuts[i].activeSelf == true)
            {
                lastIndex++;
            }
        }
        return lastIndex;
    }

    public void DonutsFalling()
    {
        int n = DonutLastControl(Donuts);
        if (n != 0)
        {
            FallingDonuts.transform.position = Donuts[DonutLastControl(Donuts)].transform.position;
            FallingDonuts.SetActive(true);
            for (int i = 0; i < n; i++)
            {
                Donuts[i].SetActive(false);
            }
        }

    }

    public IEnumerator SlapEffectDo(GameObject police)
    {
        yield return new WaitForSeconds(.3f);
        GameObject e = Instantiate(SlapEffect) as GameObject;
        e.transform.position = police.transform.position;
        PathFollower.Instance.speed = 6;
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
