using UnityEngine;
using System.Collections;

public class InflictDamageOnCollision : MonoBehaviour {

    private float damage = 20;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealth>().TakeDamage(damage);
        }
    }
}
