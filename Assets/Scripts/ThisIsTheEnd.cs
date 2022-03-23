using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsTheEnd : MonoBehaviour
{
    public List<Animator> HappyAnimationList;

    public GameObject winPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.TryGetComponent(out PlayerActionsController player))
        {
            player.gameObject.SetActive(false);
            winPlayer.SetActive(true);

            GameManager.Instance.WinGame();
        }
    }


    void MakeThemHappy(int s�n�r)
    {
        //s�n�ra kadar happy animasyonlar�n� oynat
    }
}
