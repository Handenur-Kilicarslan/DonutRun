using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Policeman : MonoBehaviour
{
    public GameObject hisDonut;
    float mesafe;
    public float stopDistance = 3f;
    public bool policeMoving = false;
    public float policeSpeed = 6f;
    public BoxCollider boxCollider;

    void Start()
    {
        boxCollider = gameObject.GetComponent<BoxCollider>();
    }
    private void Update()
    {
        //mesafe = Vector3.Distance(transform.position, LevelManager.Instance.follower.transform.position);
        if (policeMoving)
        {
            ChaseHimNoWait();
        }

    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Obstacle obstacle))
        {
            Debug.Log("Officer Down!");
            policeMoving = false;
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
        yield return new WaitForSeconds(.2f);
        transform.LookAt(lookAtHim);
        yield return new WaitForSeconds(1f);
        policeMoving = true;

        //yield return new WaitForSeconds(1f);
        //boxCollider.isTrigger = false;
    }
    void ChaseHimNoWait()
    {
        transform.LookAt(LevelManager.Instance.follower.transform);
        transform.position += transform.forward * policeSpeed * Time.deltaTime;
    }

  
}


/*
            if (mesafe > stopDistance)
            {
                //chase him i buraya koy
            }
*/

