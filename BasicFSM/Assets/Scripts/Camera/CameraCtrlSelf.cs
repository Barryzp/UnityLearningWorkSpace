using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrlSelf : MonoBehaviour
{
    public Transform target;
    public float m_dampTime = 0.2f;
    public Vector3 m_moveV;

    private Vector3 initVec;

    private void Start() {
        initVec = target.position-transform.position;
    }

    private void LateUpdate() {
        Move();
    }

    private void Move(){
        var selfPosition = transform.position;
        var aimPosition = target.position - initVec;
        transform.position = Vector3.SmoothDamp(selfPosition,aimPosition,ref m_moveV,m_dampTime);
    }
}
