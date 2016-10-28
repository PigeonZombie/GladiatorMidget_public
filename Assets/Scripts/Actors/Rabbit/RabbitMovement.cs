using UnityEngine;
using System.Collections;

public class RabbitMovement : Enemy {

    Transform player;
    NavMeshAgent nav;
    Animator anim;


    bool isJumping = false;
    bool isMoving = true;
    float jumpDistance = 40;
    float distanceFromPlayer = 0;
    float allowedJump = 10.0f;
    float groundedPosition;


	// Use this for initialization
	void Awake () {

        player = GameObject.FindGameObjectWithTag("Player").transform;
        playerHealth = player.GetComponent<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
        nav = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        groundedPosition = transform.position.y;


	}


	private void Update () {

        Move();
        if(enemyHealth.GetCurrentHealth()<=0 && transform.position.y<=groundedPosition)
        {
            enemyHealth.Die();
        }
	}

    private void FixedUpdate()
    {
        Animate();
    }

    
    protected override void Jump()
    {
        isJumping = true;
        anim.enabled = false;
        Vector3 direction = player.transform.position - transform.position;
        rb.AddForce(new Vector3(direction.x,allowedJump,direction.z), ForceMode.Impulse);

    }

    protected override void Move()
    {
        distanceFromPlayer = (player.transform.position - transform.position).sqrMagnitude;

        if (enemyHealth.GetCurrentHealth() > 0 && playerHealth.CurrentHealth > 0)
        {
            if (nav.enabled)
            {
                nav.SetDestination(player.position);
                isMoving = true;
            }

            if (distanceFromPlayer <= jumpDistance * jumpDistance && !isJumping)
            {
                nav.enabled = false;
                isMoving = false;
                Jump();
            }
            if (isJumping && rb.velocity.y < 0 && transform.position.y <= groundedPosition)
            {
                isJumping = false;
                isMoving = false;
                nav.enabled = true;
                anim.enabled = true;
            }
        }
        else 
        {
            this.enabled = false;
            nav.enabled = false;
        }
    }

    private void Animate()
    {
        anim.SetBool("IsWalking", isMoving);
    }
}
