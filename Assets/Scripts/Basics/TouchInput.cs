using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TouchInput : Singleton<TouchInput>
{
    public float horizontal;
    public float startTouch;
    public float swipeDelta;
    public float velocityX = 0f;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouch = Input.mousePosition.x;
        }
        if (Input.GetMouseButton(0))
        {
            swipeDelta = Input.mousePosition.x - startTouch;
            startTouch = Input.mousePosition.x;
        }
        if (Input.GetMouseButtonUp(0))
        {
            swipeDelta = 0f;
        }
        horizontal = Mathf.SmoothDamp(horizontal, swipeDelta, ref velocityX, 0.1f);
    }
}