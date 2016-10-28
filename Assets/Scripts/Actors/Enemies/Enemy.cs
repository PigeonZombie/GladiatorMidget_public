using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour{

    protected Rigidbody rb;
    protected EnemyHealth enemyHealth;
    protected PlayerHealth playerHealth;

    protected abstract void Move();
    protected abstract void Jump();


}
