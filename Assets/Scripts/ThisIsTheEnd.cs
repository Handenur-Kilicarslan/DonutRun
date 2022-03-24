using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThisIsTheEnd : MonoBehaviour
{
    public List<Animator> HappyAnimationList;

    public GameObject winPlayer;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerActionsController player))
        {
            //player.gameObject.GetComponent<MeshRenderer>().enabled = false;
            player.gameObject.SetActive(false);
            winPlayer.SetActive(true);

            GameManager.Instance.WinGame();
        }
    }





}
