using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTankController : MonoBehaviour
{
    public GameObject bullet;

    public float turretRotSpeed = 10.0f;
    public float maxForwardSpeed = 300.0f;
    public float maxBackwardSpeed = -300f;

    private Transform Turret;
    private Transform bulletSpawnPoint;
    private float curSpeed, targetSpeed, rotSpeed;

    protected float shootRate = 0.1f;
    protected float eplasedTime;

    // Start is called before the first frame update
    void Start()
    {
        rotSpeed = 150;

        Turret = transform.GetChild(0).transform;
        bulletSpawnPoint = Turret.transform;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateWeapon();
        UpdateControl();
    }

    void UpdateWeapon(){
        if (Input.GetMouseButtonDown(0)) { 
            eplasedTime += Time.deltaTime;
            print("eplasedTime: "+eplasedTime);
            if(eplasedTime>shootRate){
                eplasedTime = 0;

                print("Lanuch~");
                Instantiate(bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            }
        }
    }

    void UpdateControl(){
        Plane playerPlane = new Plane(Vector3.up,transform.position);
        Ray rayCast = Camera.main.ScreenPointToRay(Input.mousePosition);
        float hitDist = 0;
        if(playerPlane.Raycast(rayCast,out hitDist)){
            Vector3 hitPoint = rayCast.GetPoint(hitDist);
            Quaternion targetRotation = Quaternion.LookRotation(hitPoint - transform.position);

            Turret.rotation = Quaternion.Slerp(Turret.rotation, targetRotation, Time.deltaTime * turretRotSpeed);
        }

        if(Input.GetKey(KeyCode.W)){
            targetSpeed = maxForwardSpeed;
        }
        else if (Input.GetKey(KeyCode.S)){
            targetSpeed = maxBackwardSpeed;
        }
        else {
            targetSpeed = 0;
        }

        if(Input.GetKey(KeyCode.A)){
            transform.Rotate(0, -rotSpeed * Time.deltaTime, 0);
        }else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(0, rotSpeed * Time.deltaTime, 0);
        }

        curSpeed = Mathf.Lerp(curSpeed,targetSpeed,7.0f*Time.deltaTime);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }
}
