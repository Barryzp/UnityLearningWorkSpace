using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleFSM : FSM
{
    public enum FSMState
    {
        None,
        Patrol,
        Chase,
        Attack,
        Dead
    }

    public FSMState curState;
    private float curSpeed;
    private float curRotSpeed;
    public GameObject Bullet;
    private bool bDead;
    private int health;

    protected override void Initialize()
    {
        curState = FSMState.Patrol;
        curSpeed = 1.5f;
        curRotSpeed = 2;
        bDead = false;
        eplasedTime = 0.0f;
        shootRate = 1.0f;
        health = 100;

        pointList = GameObject.FindGameObjectsWithTag("WanderPoint");
        FindNextPoint();
        GameObject objPlayer = GameObject.FindGameObjectWithTag("Player");
        playerTransform = objPlayer.transform;
        if (!playerTransform) print("Player doesn't exist!");

        turret = transform.GetChild(0).transform;
        bulletSpawnPoint = turret;
    }

    protected override void FSMUpdate()
    {
        switch (curState)
        {
            case FSMState.Patrol:
                UpdatePatrolState();
                break;
            case FSMState.Chase:
                UpdateChaseState();
                break;
            case FSMState.Attack:
                UpdateAttackState();
                break;
            case FSMState.Dead:
                UpdateDeadState();
                break;
        }

        eplasedTime += Time.deltaTime;
        if (health <= 0) curState = FSMState.Dead;
    }

    protected void UpdatePatrolState()
    {
        var distanceFormPlayer = Vector3.Distance(transform.position, playerTransform.position);
        var distanceWanderingPoint = Vector3.Distance(transform.position, destPos);
        // print("distanceFormPlayer: "+distanceFormPlayer);
        // print("distanceWanderingPoint: "+distanceWanderingPoint);

        if ( distanceWanderingPoint <= 1)
        {
            print("到达巡逻点\n寻找下一个点.");
            FindNextPoint();
        }
        else if (distanceFormPlayer <= 2.5)
        {
            print("转换状态至Chase.");
            curState = FSMState.Chase;
        }

        Quaternion targetRotation = Quaternion.LookRotation(destPos - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateChaseState()
    {
        destPos = playerTransform.position;

        float dist = Vector3.Distance(transform.position, destPos);
        if (dist < 2)
        {
            curState = FSMState.Attack;
        }
        else if (dist >= 3)
        {
            curState = FSMState.Patrol;
        }

        transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
    }

    protected void UpdateAttackState()
    {
        destPos = playerTransform.position;
        Vector3 selfPosition = transform.position;

        float dist = Vector3.Distance(destPos, selfPosition);
        if (dist >= 2 && dist < 3)
        {
            Quaternion targetRotation = Quaternion.LookRotation(destPos - selfPosition);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * curRotSpeed);
            transform.Translate(Vector3.forward * Time.deltaTime * curSpeed);
            curState = FSMState.Attack;
        }
        else if (dist >= 3)
        {
            curState = FSMState.Patrol;
        }

        Quaternion turretRotation = Quaternion.LookRotation(destPos - selfPosition);
        turret.rotation = Quaternion.Slerp(turret.rotation, turretRotation, Time.deltaTime * curRotSpeed);

        ShootBullet();
    }

    private void ShootBullet()
    {
        if (eplasedTime >= shootRate)
        {
            Instantiate(Bullet, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
            eplasedTime = 0.0f;
        }
    }

    protected void UpdateDeadState()
    {
        if (!bDead)
        {
            bDead = true;
            Explode();
        }
    }

    protected void Explode()
    {
        float rdX = Random.Range(1, 3);
        float rdZ = Random.Range(1, 3);
        print("Explode!");
        for (int i = 0; i < 3; i++)
        {

        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Bullet")
        {
            health -= other.gameObject.GetComponent<Bullet>().damage;
        }
    }

    #region 寻找下一个点
    protected void FindNextPoint()
    {
        print("starting find next point!");
        float rdRadius = 3.0f;
        int rdIndex = Random.Range(0, pointList.Length);
        Vector3 rdPosition = pointList[rdIndex].transform.position;
        destPos = rdPosition;
        if (IsInCurrentRange(destPos))
        {
            rdPosition = new Vector3(Random.Range(-rdRadius, rdRadius), 0, Random.Range(-rdRadius, rdRadius));
            destPos = pointList[rdIndex].transform.position + rdPosition;
        }
    }

    protected bool IsInCurrentRange(Vector3 pos)
    {
        float xPos = Mathf.Abs(pos.x - transform.position.x);
        float zPos = Mathf.Abs(pos.z - transform.position.z);
        if (xPos <= 5 && zPos <= 5)
        {
            return true;
        }
        return false;
    }
    #endregion
}
