using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerActionsController player))
        {
            player.gameObject.SetActive(false);
            LevelManager.Instance.ragDoll.SetActive(true);
        }
    }



}
