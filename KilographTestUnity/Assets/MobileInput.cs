using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{

    private bool gyroState;
 
    private float width;
    private float height;

    private Vector2 firstPoint;
    private Vector2 secondPoint;

    private float x;
    private float y;

    private float xTemp;
    private float yTemp;

    private Vector3 distance;

    // Start is called before the first frame update
    void Start()
    {
        width = (float)Screen.width / 2.0f;
        height = (float)Screen.height / 2.0f;

        gyroState = false;

        x = 0.0f;
        y = 0.0f;       
    }

    // Update is called once per frame
    void Update()
    {
        if (gyroState)
        {
            GyroRotation();
        }
        else
        {
            TouchRotation();
        }
    }

    private void TouchRotation()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                firstPoint = Input.GetTouch(0).position;
                xTemp = x;
                yTemp = y;
            }

            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                secondPoint = Input.GetTouch(0).position;
                x = (float)(xTemp + (secondPoint.x - firstPoint.x) * 180.0f / (float)Screen.width) * .01f;
                y = (float)(yTemp + (secondPoint.y - firstPoint.y) * 90.0f / (float)Screen.height) * .01f;
                transform.rotation = Quaternion.Euler(x, y, 0.0f);
            }
        }

    }

    private void GyroRotation()
    {
        transform.rotation = GyroToUnity(Input.gyro.attitude);
    }

    private Quaternion GyroToUnity(Quaternion q)
    {
        return new Quaternion(q.x, q.y, -q.z, -q.w);
    }

    public void SwitchGyroState()
    {
        if (gyroState)
        {
            gyroState = false;
        }
        else
        {
            gyroState = true;
        }

        Debug.Log(gyroState);
    }
}
