using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TrajectoryPrediction : MonoBehaviour
{
    
    public Transform target;
    public Transform Ball;
    public float ShootingAngle = 30f;
    public float speed= 10f;
    public bool MakeTile = true;
    public Material TrailMat;
    public Vector3 NeededVelocity;
    public Vector3 NeededForce;
    private void Start()
    {
        //  CalculateSpeed(Ball.position, target.position, ShootingAngle);
        //CalculateSpeed(Ball.position, target.position, 60);
    }
    Vector3 CalculateForce()
    {
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        Vector3 Force = new Vector3();
        Vector3 Velocity =CalculateSpeed(Ball.position, target.position, ShootingAngle);
        Vector3 acceleration = (Velocity - rb.velocity) / Time.fixedDeltaTime;
        Force = acceleration * rb.mass;
        NeededForce = Force;
        return Force;

    }
    private void FixedUpdate()
    {
    }
    public void Shoot()
    {
        if (MakeTile)addTail();
        Rigidbody rb = Ball.GetComponent<Rigidbody>();
        rb.useGravity = true;
        //  rb.velocity = CalculateSpeed(Ball.position, target.position, ShootingAngle) ;
        rb.AddForce(CalculateForce());
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
        NeededVelocity = velocity;
        return velocity;
    }

    void addTail()
    {
        Ball.gameObject.AddComponent<TrailRenderer>();
        Ball.GetComponent<TrailRenderer>().startWidth = 0.1f;
        Ball.GetComponent<TrailRenderer>().material = TrailMat;
    }
    public void OnClickPridection()
    {
        StartCoroutine(prediction());
    }
    public IEnumerator prediction()
    {
        Ball.GetComponent<Collider>().enabled = false;
        Transform Original = Ball;
        GameObject ghost = Instantiate(Ball.gameObject, Ball.position, Ball.rotation);
        this.Ball = ghost.transform;
        addTail();
        Ball.GetComponent<Collider>().enabled = true;
        Ball.GetComponent<MeshRenderer>().enabled = false;
        NeededVelocity = CalculateSpeed(Ball.position, target.position, ShootingAngle);
        Shoot();
        this.Ball = Original;
        yield return new WaitForSeconds(0.1f);
        Ball.GetComponent<Collider>().enabled = true;
    }
}
