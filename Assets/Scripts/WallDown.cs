using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WallDown : MonoBehaviour
{
    public GameObject myWall;

    public void MakeWallFallDown()
    {
        Debug.Log("Wall is Down");
        myWall.transform.DOMoveY(myWall.transform.position.y - 5, 2f);
    }
   
}
