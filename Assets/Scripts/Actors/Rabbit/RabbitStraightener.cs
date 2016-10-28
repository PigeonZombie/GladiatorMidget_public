using UnityEngine;
using System.Collections;

public class RabbitStraightener : MonoBehaviour {

    private float xRotationLimit = 0.2f;
    private float zRotationLimit = 0.2f;
    private float straightenSpeed = 0.1f;
    private Quaternion newRot;

    private Transform t;
    private Rigidbody r;
    private EnemyHealth health;

    private void Start()
    {
        t = transform.parent.GetComponent<Transform>();
        r = transform.parent.GetComponent<Rigidbody>();
        health = transform.parent.GetComponent<EnemyHealth>();
        newRot = Quaternion.identity;
    }
	

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor" )
        {
            t.rotation = Quaternion.Lerp(t.rotation, newRot, Time.time * straightenSpeed);
            r.velocity.Set(0, 0, 0);
        }
    }
}
