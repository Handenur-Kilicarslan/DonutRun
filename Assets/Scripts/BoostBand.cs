using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostBand : MonoBehaviour
{
    public List<GameObject> WallCubes;
    // Start is called before the first frame update
  

    public void FallingWallCubes()
    {
        if(WallCubes != null)
        {
            for (int i = 0; i < WallCubes.Count; i++)
            {

                WallCubes[i].gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                WallCubes[i].gameObject.GetComponent<Rigidbody>().useGravity = true;
            }
        }
    }
}
