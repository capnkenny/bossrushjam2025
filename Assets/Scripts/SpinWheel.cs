using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public float RotatePower;
    public float StopPower;

    private Rigidbody2D rbody;
    int inRotate;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }

    float t;
    void Update()
    {
        if (rbody.angularVelocity > 0)
        {
            rbody.angularVelocity -= StopPower * Time.deltaTime;

            rbody.angularVelocity = Mathf.Clamp(rbody.angularVelocity, 0, 1440);
        }

        if (rbody.angularVelocity == 0 && inRotate == 1)
        {
            t += 1 * Time.deltaTime;
            if (t >= 0.5f)
            {
                GetReward();

                inRotate = 0;
                t = 0;
            }
        }
    }

    public void Rotate()
    {
        if (inRotate == 0)
        {
            rbody.AddTorque(RotatePower);
            inRotate = 1;
        }
    }

    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 0 && rot <= 90)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90);
        }
        else if (rot > 90 && rot <= 180)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);
        }
        else if (rot > 180 && rot <= 270)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 270);
        }
        else if (rot > 270 && rot <= 360)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 360);
        }

    }
}
