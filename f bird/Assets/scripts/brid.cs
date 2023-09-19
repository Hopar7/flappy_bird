using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brid : MonoBehaviour
{
    public float FlyPower;  //스페이스키 눌렀을때 위로 올라가는 힘
    public float Velocity;  //속력
    public float Gravity;   //아래로 떨어지는 중력


    private float minPo = -0.6f;

    public void Init()
    {
        Velocity = 0;
        transform.localPosition = Vector3.zero;
        transform.rotation = Quaternion.identity;
    }



    public void fly()
    {
        Velocity = Velocity - Gravity *Time.deltaTime;


        if(Velocity>FlyPower)
        {
            Velocity = FlyPower;
        }
        
        if(Velocity <-FlyPower)
        {
            Velocity = -FlyPower;
        }

        transform.localPosition += new Vector3(0, Velocity, 0);

        if (IsBelowFloor())
        {
            transform.localPosition = new Vector3(0, minPo, 0);
        }



        RotateBody();
    }

    public void Jump()
    {
        Velocity = FlyPower;
    }

    public void RotateBody()
    {
        float ratio = Velocity / FlyPower;
        float angle = ratio * 90;

        if(angle >30)
        {
            angle = 30;
        }    

        transform.localRotation = Quaternion.Euler(0,0,angle);
    }


    public bool IsBelowFloor()
    {
        if (transform.localPosition.y <= minPo)
        {
            return true;
        }

        return false;
    }

  



}
