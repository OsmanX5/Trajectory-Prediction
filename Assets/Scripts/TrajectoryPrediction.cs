using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryPrediction : MonoBehaviour
{
    public Transform target;
    public Transform Ball;
    public float ShootingAngle = 30f;
    public float speed= 10f;
    private void Start()
    {
        //  CalculateSpeed(Ball.position, target.position, ShootingAngle);
        //CalculateSpeed(Ball.position, target.position, 60);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }
    public void Shoot()
    {
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        rb.velocity = CalculateSpeed(Ball.position, target.position, ShootingAngle) ;
    }
    public Vector3 CalculateSpeed(Vector3 shooter,Vector3 target,float angle)
    {
        angle = Mathf.Deg2Rad * angle;
        Vector3 dir = target - shooter;
        Vector3 xzdir = new Vector3(dir.x,0, dir.z);
        Debug.Log(dir);
        float  y, x ,g ,u,uxz,uy,ux,uz,tana,cosa,sina;
        g = -Physics.gravity.y;
        x = xzdir.magnitude;
        tana = Mathf.Tan(angle);
        cosa = Mathf.Cos(angle);
        sina = Mathf.Sin(angle);
        y = dir.y;
        uxz = (g*x*x) / (2 * (tana *x - y) );
      /*  Debug.Log("x = " + x);
        Debug.Log("g = " + g);
        Debug.Log("y = " + y);
        Debug.Log("ux = " + ux);
        Debug.Log(ux);*/
        uxz = Mathf.Sqrt(uxz);
        u = uxz / cosa;
        uy = u * sina;
        ux = uxz * xzdir.normalized.x;
        uz = uxz * xzdir.normalized.z;
        Debug.Log(u);
        Debug.Log(uy);
        Vector3 velocity = new Vector3(ux,uy,uz);
        return velocity;
    }

}
