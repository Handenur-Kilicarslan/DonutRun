
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlayerActionsController : MonoBehaviour
{
    public List<GameObject> Donuts;

    public Transform donutParent;

    [Header("Scripts to be disabled")]
    public DonutMoveController donutMoveController;
    public SMoveController stackMove;
    [Header("Effects")]
    public GameObject FallingDonuts;
    public GameObject SlapEffect;
    private void Start()
    {
        for (int i = 0; i < Donuts.Count; i++)
        {
            Donuts[i].SetActive(false);
        }
    }
    private void Update()
    {
        //Debug.Log("Active Donuts : " + DonutLastControl(Donuts));
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
            //PlayerAnimController.Instance.RunWithDonutsToWalk();
            PathFollower.Instance.speed -= .8f;
            StartCoroutine(DistributeDonutsandStopMoving());

        }

        if (other.gameObject.TryGetComponent(out SignBoard signBoard))
        {
            int n = signBoard.donutFallCount;
            Debug.Log(n + "hangi donuttan itibaren düssün" + "/n" + DonutLastControl(Donuts) + "Acýk olan donutlar");
            if (n <= DonutLastControl(Donuts))
            {
                for (int i = n; i < Donuts.Count; i++)
                {
                    Donuts[i].SetActive(false);
                    //onun transformundan donutlarý düþür ama donut varsa activeself ile kontrol edersin
                }

                FallingDonuts.transform.position = Donuts[DonutLastControl(Donuts)].transform.position + new Vector3(0, 1, -2);
                FallingDonuts.transform.parent = null;
                FallingDonuts.SetActive(true);
            }

        }

        if (other.gameObject.TryGetComponent(out WallDown wall))
        {
            wall.MakeWallFallDown();
        }

        if (other.gameObject.TryGetComponent(out BoostBand boost))
        {
            StartCoroutine(SpeedUp(3f, 3f));
        }

        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("This is Policeman");
            police.GetHisDonut(transform);
            if(police.haveOneDonut)
            {
                DOVirtual.DelayedCall(.3f, () => Donuts[DonutLastControl(Donuts)].SetActive(true));
                police.haveOneDonut = false;
                StartCoroutine(SlapEffectDo(police.gameObject));
            }

            if (DonutLastControl(Donuts) <= 1)
            {
                PlayerAnimController.Instance.WalkToSlap();
            }
            else if (DonutLastControl(Donuts) > 1)
            {
                PlayerAnimController.Instance.RunDonutToRunDonutSlap();
            }

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
            else if (DonutLastControl(Donuts) >= 1)
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
        PathFollower.Instance.speed = 6; //PathFollower Speed
    }

    public IEnumerator SpeedUp(float speedAdd, float duration)
    {
        float x = speedAdd / 3;
        PathFollower.Instance.speed += speedAdd;
        yield return new WaitForSeconds(duration);

        PathFollower.Instance.speed -= x;

        yield return new WaitForSeconds(1f);
        PathFollower.Instance.speed -= x;

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
