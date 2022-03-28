using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Policeman : MonoBehaviour
{
    public GameObject hisDonut;
    float mesafe;
    private float stopDistance = 4.2f;
    public bool policeMoving = false;
    public static bool allPoliceMoving = true;
    public float policeSpeed = 6f;
    public CapsuleCollider capsuleCollider;
    public Animator policeAnimation;
    public bool haveOneDonut = true;



    void Start()
    {
        capsuleCollider = gameObject.GetComponent<CapsuleCollider>();
        policeAnimation = GetComponent<Animator>();
        allPoliceMoving = true;
    }
    private void Update()
    {
        mesafe = Vector3.Distance(transform.position, LevelManager.Instance.follower.transform.position);

        if (policeMoving && allPoliceMoving && mesafe > stopDistance)
        {

            if (PlayerActionsController.Instance.SpeedUpPolice)
            {
                Debug.Log("Player Hýzlandý");
                StartCoroutine(ChaseHimWithSpeedIEN());
            }
            else
            {
                Debug.Log("Player Normal Hýzda gidiyor");
                ChaseHimWithSpeed(policeSpeed);
            }
        }
        else if (allPoliceMoving == false)
        {
            StartCoroutine(StopRunningPoliceman());
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("Officer Down!");
            policeMoving = false;
        }

        if (collision.gameObject.TryGetComponent(out Policeman anotherPolice))
        {
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), anotherPolice.GetComponent<CapsuleCollider>());
        }

        if (collision.gameObject.TryGetComponent(out PlayerActionsController player))
        {
            Physics.IgnoreCollision(GetComponent<CapsuleCollider>(), player.GetComponent<BoxCollider>());

        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("Officer Down!");
            policeMoving = false;
        }
    }

    public void GetHisDonut(Transform lookAtHim)
    {
        Debug.Log("He stole my donut! Chase Him!");
        hisDonut.SetActive(false);
        StartCoroutine(ChaseHim(lookAtHim));
    }



    public IEnumerator ChaseHim(Transform lookAtHim)
    {
        yield return new WaitForSeconds(.3f);
        transform.LookAt(lookAtHim);

        yield return new WaitForSeconds(.4f);
        capsuleCollider.isTrigger = false;

        yield return new WaitForSeconds(.5f);
        policeMoving = true;



    }
    void ChaseHimWithSpeed(float policeSpeed)
    {
        policeAnimation.SetBool("isRunning", true);
        transform.LookAt(LevelManager.Instance.follower.transform);
        transform.position += transform.forward * policeSpeed * Time.deltaTime;
    }

    IEnumerator ChaseHimWithSpeedIEN()
    {
        ChaseHimWithSpeed(policeSpeed + 4f);
        yield return new WaitForSeconds(3f);
        PlayerActionsController.Instance.SpeedUpPolice = false;
    }
    public IEnumerator StopRunningPoliceman()
    {
        policeSpeed = 0.5f;
        transform.LookAt(LevelManager.Instance.follower.transform);
        capsuleCollider.isTrigger = true;
        Rigidbody rb = GetComponent<Rigidbody>();
        yield return new WaitForSeconds(.6f);
        policeAnimation.SetBool("isRunning", false);

        rb.isKinematic = true;
        policeMoving = false;
        yield return new WaitForSeconds(.3f);

    }

}


/*
            if (mesafe > stopDistance)
            {
                //chase him i buraya koy
            }
*/

