using UnityEngine;
using System.Collections;

public class PlayerHealth : MonoBehaviour {

    [SerializeField]
    private float cur_Health = 100f;
    [SerializeField]
    private float max_health = 100f;
    [SerializeField]
    private GameObject healthBar;

    private bool isDead;

    public delegate void PlayerDeathHandler();
    public event PlayerDeathHandler OnPlayerDeath;

    public float CurrentHealth
    {
        get { return cur_Health; }
        set { cur_Health = value; }
    }


    private void Start()
    {
        isDead = false;

    }


    private void SetHealthBar(float normalized_health)
    {
        if (normalized_health >= 0)
            healthBar.transform.localScale = new Vector3(normalized_health, 1, 1);
        else
            healthBar.transform.localScale = new Vector3(0, 1, 1);
    }


    public void TakeDamage(float amount)
    {
        if (!isDead)
        {
            cur_Health -= amount;
            SetHealthBar(cur_Health / max_health);
            if (cur_Health <= 0)
                Die();
        }

    }

    private void Die()
    {
        isDead = true;
        if (OnPlayerDeath != null)
            OnPlayerDeath();
        //anim.SetTrigger("Dead");
    }

    public bool IsDead()
    {
        return isDead;
    }
}
