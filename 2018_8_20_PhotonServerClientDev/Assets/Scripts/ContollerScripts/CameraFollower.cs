using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour {

    public Transform aimTransfrom;
    private Vector3 offset;

	void Start () {
        offset = this.transform.position - aimTransfrom.position;
	}
	
	void LateUpdate () {
        this.transform.position = aimTransfrom.position + offset;
	}
}
