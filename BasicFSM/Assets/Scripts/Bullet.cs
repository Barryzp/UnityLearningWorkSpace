using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject Explosion;

    public float speed = 3.0f;
    public float lifeTime = 3.0f;
    public int damage = 50;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnCollisionEnter(Collision collision) {
        ContactPoint ctP = collision.contacts[0];
        Instantiate(Explosion, ctP.point, Quaternion.identity);
        Destroy(gameObject);
    }
}
