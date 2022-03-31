
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TapticPlugin;

public class PlayerActionsController : Singleton<PlayerActionsController>
{
    public List<GameObject> Donuts;
    public Transform donutParent;

    [Header("Scripts to be disabled")]
    public DonutMoveController donutMoveController;
    public SMoveController stackMove;

    [Header("Effects")]
    public List<GameObject> FallingDonutsList;
    public GameObject fastTrail;
    public GameObject FallingDonuts;
    public GameObject SlapEffect;

    [Header("Booleans")]
    public bool SpeedUpPolice;
    private void Start()
    {
        fastTrail.SetActive(false);
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

            TapticPlugin.TapticManager.Impact(ImpactFeedback.Heavy);
            DonutsFalling();
            DOVirtual.DelayedCall(.5f, () => Policeman.allPoliceMoving = false);
            GameManager.Instance.LoseGame();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("End"))
        {
            Policeman.allPoliceMoving = false;

            PathFollower.Instance.speed += 4f;
            StartCoroutine(DistributeDonutsandStopMoving());

        }

        if (other.gameObject.TryGetComponent(out Slower slowedObject))
        {
            StartCoroutine(SpeedDown(3f, 1.5f));
            if (fastTrail.activeSelf)
            {
                fastTrail.SetActive(false);
            }
        }

        if (other.gameObject.TryGetComponent(out SignBoard signBoard))
        {
            int n = signBoard.donutFallCount;   //Debug.Log(n + "hangi donuttan itibaren düssün" + "/n" + DonutLastControl(Donuts) + "Acýk olan donutlar");

            if (n <= DonutLastControl(Donuts))
            {
                for (int i = n; i < Donuts.Count; i++)
                {
                    Donuts[i].SetActive(false);  //onun transformundan donutlarý düþür ama donut varsa activeself ile kontrol edersin
                }

                if (signBoard.isCrashed)
                {
                    GameObject FallingDonuts2 = Instantiate(FallingDonuts) as GameObject;
                    FallingDonuts2.transform.position = Donuts[DonutLastControl(Donuts)].transform.position + new Vector3(0, 2, -2);
                    TapticPlugin.TapticManager.Impact(ImpactFeedback.Light);
                    signBoard.isCrashed = false;
                }


                /*
                FallingDonuts.transform.position = Donuts[DonutLastControl(Donuts)].transform.position + new Vector3(0, 1, -2);
                FallingDonuts.transform.parent = null;
                FallingDonuts.SetActive(true);
                */
            }

        }

        if (other.gameObject.TryGetComponent(out WallDown wall))
        {
            wall.MakeWallFallDown();
        }

        if (other.gameObject.TryGetComponent(out BoostBand boost))
        {
            StartCoroutine(SpeedUp(4f, 3f));
            SpeedUpPolice = true;

        }

        if (other.gameObject.TryGetComponent(out Policeman police))
        {
            Debug.Log("This is Policeman");

            if (police.haveOneDonut)
            {
                TapticPlugin.TapticManager.Impact(ImpactFeedback.Medium);
                police.GetHisDonut(transform);
                DOVirtual.DelayedCall(.3f, () => Donuts[DonutLastControl(Donuts)].SetActive(true));
                police.haveOneDonut = false;

                StartCoroutine(SlapEffectDo(police.gameObject));
            }

            if (DonutLastControl(Donuts) < 1)
            {
                PlayerAnimController.Instance.WalkToSlap();
                PathFollower.Instance.speed = 6.5f; //--------------------------------------------------
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
            else if (DonutLastControl(Donuts) > 1)
            {
                PlayerAnimController.Instance.RunDonutSlapExit();
            }

            //PathFollower.Instance.speed = 6; //PathFollower Speed
        }
    }



    public IEnumerator DistributeDonutsandStopMoving()
    {
        transform.parent.transform.DOLocalMoveX(-0.908f, .5f);
        yield return new WaitForSeconds(.3f);
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

        for (int i = 0; i < n; i++)
        {
            Donuts[i].SetActive(false);
        }

        if (n != 0 )
        {
            for (int i = 0; i < n; i++)
            {
                FallingDonutsList[i].SetActive(true);
            }
        }

            //GameObject FallingDonuts2 = Instantiate(FallingDonuts) as GameObject;
           //FallingDonuts2.transform.position = Donuts[DonutLastControl(Donuts)].transform.position;
        

    }


    public IEnumerator SlapEffectDo(GameObject police)
    {
        yield return new WaitForSeconds(.3f);
        GameObject e = Instantiate(SlapEffect) as GameObject;
        e.transform.position = police.transform.position;

    }

    public IEnumerator SpeedUp(float speedAdd, float duration)
    {
        float x = speedAdd / 2;
        fastTrail.SetActive(true);
        PathFollower.Instance.speed += speedAdd;
        yield return new WaitForSeconds(duration);

        PathFollower.Instance.speed -= x;

        yield return new WaitForSeconds(.5f);
        PathFollower.Instance.speed -= x;

        fastTrail.SetActive(false);

    }

    public IEnumerator SpeedDown(float speedSub, float duration)
    {

        PathFollower.Instance.speed -= speedSub;
        yield return new WaitForSeconds(duration);
        PathFollower.Instance.speed += speedSub;

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
