using UnityEngine;
using System.Collections;

public class RabbitAttack : MonoBehaviour{

    [SerializeField]
    private float damage = 2.0f;

    private Transform player;
    private PlayerHealth playerHealth;
    private EnemyHealth health;
    

    private void Start()
    {

        health = GetComponent<EnemyHealth>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
	}


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && health.GetCurrentHealth()>0)
        {
            playerHealth.TakeDamage(damage);
        }
    }

}
