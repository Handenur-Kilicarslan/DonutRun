using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policeman : MonoBehaviour
{
    public GameObject hisDonut;
    float mesafe;
    public float stopDistance = 3f;
    public static bool policeMoving = false;
    public float policeSpeed = 6f;
    public BoxCollider boxCollider;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    private void Update()
    {
        if(policeMoving)
        {
            ChaseHimNoWait();
        }
        
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            policeMoving = false;
        }
    }

    public void GetHisDonut(Transform lookAtHim)
    {
        //mesafe = Vector3.Distance(transform.position, lookAtHim.position);
        Debug.Log("He stole my donut! Chase Him!");
        hisDonut.SetActive(false);
        StartCoroutine(ChaseHim(lookAtHim));

    }
    void ChaseHimNoWait()
    {
        transform.LookAt(LevelManager.Instance.follower.transform);
        transform.position += transform.forward * policeSpeed * Time.deltaTime;
    }

    public IEnumerator ChaseHim(Transform lookAtHim)
    {
        yield return new WaitForSeconds(.2f);
        transform.LookAt(lookAtHim);
        yield return new WaitForSeconds(1f);
        policeMoving = true;

        yield return new WaitForSeconds(1f);
        //boxCollider.isTrigger = false;
    }
}


/*
 if(mesafe > stopDistance)
        {
            transform.position += transform.forward * 3f * Time.deltaTime;
        }*/
