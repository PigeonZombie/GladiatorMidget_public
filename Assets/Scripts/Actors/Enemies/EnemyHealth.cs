using UnityEngine;
using System.Collections;

public class EnemyHealth : MonoBehaviour {

    public float max_health = 40;
    public GameObject healthBarPrefab;
    private GameObject healthBar;
    private GameObject health;
    float cur_health;
    private float groundedYPos;

    private Vector3 position = Vector3.zero;
    private Quaternion rotation = Quaternion.identity;
    private float minYPosition;

    Animator anim;
    bool isDead;
    bool isSinking;
    float sinkSpeed = 2.5f;
    private float sinkDelay = 3f;
    private float waitTime = 0;

    private EnemyManager _enemyManager;

	void Awake () {
	    anim = GetComponent<Animator>();
        groundedYPos = transform.position.y;
        cur_health = max_health;
        healthBar = Instantiate(healthBarPrefab);
        health = healthBar.transform.FindChild("Health").gameObject;
        position = transform.FindChild("HealthUI").gameObject.transform.position;
        rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
        minYPosition = position.y;

        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        
	}
	
	void Update () {

        if (isSinking)
        {
            waitTime += Time.deltaTime;
            if (waitTime >= sinkDelay)
            {
                Vector3 rotation = new Vector3(transform.rotation.x * 2, 0f, 0f);
                transform.Translate((-Vector3.up - rotation) * sinkSpeed * Time.deltaTime);
            }
        }
        else
        {
            position.x = transform.position.x;
            if (transform.position.y >= minYPosition)
                position.y = transform.position.y;
            position.z = transform.position.z;
            healthBar.transform.position = position;

            this.rotation.y = transform.rotation.y;
            healthBar.transform.rotation = this.rotation;

            if (cur_health <= 0)
                Die();
        }
	}

    public void TakeDamage(float amount)
    {
        if(!isDead)
        {
            cur_health -= amount;
            SetHealthBar(cur_health/max_health);
            if (cur_health <= 0)
                Die();
        }
    }

    public void Die()
    {
        if (!isDead && transform.position.y <= groundedYPos)
        {
            Debug.Log("Dead");
            isDead = true;
            anim.enabled = true;
            anim.SetTrigger("Die");
            
            StartSinking();
        }
    }

    void StartSinking()
    {
        _enemyManager.EnemyDied();
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        isSinking = true;
        Destroy(gameObject, sinkDelay*2);
        Destroy(healthBar);
    }

    public float GetCurrentHealth()
    {
        return cur_health;
    }

    void SetHealthBar(float normalized_health)
    {
        if (normalized_health >= 0)
            health.transform.localScale = new Vector3(normalized_health, 1, 1);
        else
            health.transform.localScale = new Vector3(0, 1, 1);
    }
 

}
