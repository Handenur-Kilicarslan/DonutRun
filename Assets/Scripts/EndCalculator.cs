using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class EndCalculator : MonoBehaviour
{
    private int donutCount;
    public GameObject finalEndTrigger;
    public List<GameObject> PoorPeoples;
    public List<Animator> HappyAnimationList;
    int LastDonutPos;

    private void Start()
    {
        for (int i = 0; i < PoorPeoples.Count ; i++)
        {
            HappyAnimationList[i] = PoorPeoples[i].GetComponent<Animator>();
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerActionsController player))
        {
            CameraManager.Instance.SwitchWinCamera();
            donutCount = player.DonutLastControl(player.Donuts);

            StartCoroutine(DistributeDonuts(player.Donuts, donutCount, player.donutParent));
            
            LastDonutPos = donutCount;
            finalEndTrigger.transform.position =
                new Vector3(finalEndTrigger.transform.position.x, finalEndTrigger.transform.position.y, PoorPeoples[LastDonutPos].transform.position.z);
        
        }
    }


    public IEnumerator DistributeDonuts(List<GameObject> Donuts, int donutCount, Transform parentDonut)
    {
        int n = 0;
        Vector3 poorPeoplePos;
        Debug.Log("DonutCount : " + donutCount);
        for (int i = donutCount - 1; i >= 0; i--)
        {
            Donuts[i].transform.parent = parentDonut;

            poorPeoplePos = PoorPeoples[n].transform.GetChild(0).transform.position;
            Donuts[i].transform.DOMove(poorPeoplePos, .7f);

            HappyAnimationList[n].SetBool("beHappy", true);

            yield return new WaitForSeconds(.4f);
            n++;
            Debug.Log("Poor peoples : " + n);
        }

    }




}
