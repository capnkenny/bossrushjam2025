using System.Linq;
using UnityEngine;

public class SpinWheel : MonoBehaviour
{
    public GameManager gm;

    public float RotatePower;
    public float StopPower;

    private Rigidbody2D rbody;
    private bool spinning;

    int inRotate;

    void Start()
    {
        var list = FindObjectsByType<GameManager>(FindObjectsSortMode.None);
        if (list != null && list.Length != 0)
        {
            gm = (GameManager)list.First();
        }

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
        int currency = gm.GetPlayerCurrency();

        if (!spinning)
        {
            if (currency >= 20)
            {
                gm.AddToCurrency(-20);
            }
            else
            {
                return;
            }
            if (inRotate == 0)
            {
                rbody.AddTorque(RotatePower * Random.Range(.5f, 3f));
                inRotate = 1;
            }

            spinning = true;
        }

    }

    public void GetReward()
    {
        float rot = transform.eulerAngles.z;

        if (rot > 45 && rot <= 135)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 90);
            //Attack powerup increase
            gm.PlayerPowerUpMode++;
        }
        else if (rot > 135 && rot <= 225)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 180);
            //Speed powerup increase
            gm.PlayerSpeedRate += 0.2f;
        }
        else if (rot > 225 && rot <= 315)
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 270);
            gm.PlayerHealth.DmgUnit(1);
        }
        else if ((rot > 315 && rot <= 360) || (rot > 0 && rot <= 45))
        {
            GetComponent<RectTransform>().eulerAngles = new Vector3(0, 0, 360);
            gm.PlayerHealth._currentMaxHealth ++;
            gm.PlayerHealth.HealUnit(1);
        }

        spinning = false;

    }

}
