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

            StartCoroutine(DistributeDonuts(player.Donuts, donutCount - 1, player.donutParent));
            
            LastDonutPos = donutCount;
            finalEndTrigger.transform.position =
                new Vector3(finalEndTrigger.transform.position.x, finalEndTrigger.transform.position.y, PoorPeoples[LastDonutPos].transform.position.z);
        
        }
    }


    public IEnumerator DistributeDonuts(List<GameObject> Donuts, int donutCount, Transform parentDonut)
    {
        int n = 0; //donutlar� �stten(sondan)da��tt��m ama insanlara ilk s�radan verdi�im i�in ayr� bir n de�i�kenini artt�raca��m
        Vector3 poorPeoplePos;

        for (int i = donutCount; i >= 0; i--)
        {
            Donuts[i].transform.parent = parentDonut; //parent� ile hareketine devam etmesin diye parent'i de�i�tiriyorum

            poorPeoplePos = PoorPeoples[n].transform.GetChild(0).transform.position; //targetlar�n konumunu al�yorum

            Donuts[i].transform.DOMove(poorPeoplePos, .45f); //targetlara gidiyor

            HappyAnimationList[n].SetBool("beHappy", true);

            yield return new WaitForSeconds(.5f);
            n++;
          
        }

    }


    //Debug.Log("DonutCount : " + donutCount);
    //Debug.Log("Poor peoples : " + n);


}
