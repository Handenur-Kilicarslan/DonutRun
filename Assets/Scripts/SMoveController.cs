using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SMoveController : MonoBehaviour
{
    public List<GameObject> Followers = new List<GameObject>();
    Vector3 velocity = Vector3.zero;

    void Update()
    {
        for (int i = 0; i < Followers.Count; i++)
        {
            if (i > 0)
            {
                Followers[i].transform.position =
               Vector3.SmoothDamp(Followers[i].transform.position, new Vector3(Followers[i - 1].transform.position.x, Followers[i].transform.position.y, Followers[i].transform.position.z), ref velocity, 0.015f);

            }
        }
    }
}
