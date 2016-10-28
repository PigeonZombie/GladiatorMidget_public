using UnityEngine;
using System.Collections;

public class EnemyManager : MonoBehaviour {


    private PlayerHealth playerHealth;
    [SerializeField]
    private float spawnRate = 3f;
    [SerializeField]
    private GameObject enemy;
    [SerializeField]
    private Transform[] spawnPoints;
    [SerializeField]
    private int maxSimultaneousEnemies;
    [SerializeField]
    private int totalEnemiesToSpawn;

    public delegate void PlayerWonHandler();
    public event PlayerWonHandler OnPlayerWon;

    private int enemyCounter = 0;
    private int enemiesToKill = 0;
    private int casualtiesCounter;

	private void OnEnable () {

        InvokeRepeating("Spawn", spawnRate, spawnRate);
	    playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();

	    enemiesToKill = totalEnemiesToSpawn;
	}


    private void Spawn()
    {
        if (totalEnemiesToSpawn > 0)
        {
            if (playerHealth.CurrentHealth > 0 && enemyCounter < maxSimultaneousEnemies)
            {
                enemyCounter++;
                int spawnPointIndex = Random.Range(0, spawnPoints.Length);
                Instantiate(enemy, spawnPoints[spawnPointIndex].position, spawnPoints[spawnPointIndex].rotation);
                totalEnemiesToSpawn--;
            }
        }
    }

    public void EnemyDied()
    {
        casualtiesCounter++;
        enemyCounter--;
        Debug.Log(casualtiesCounter+"/"+enemiesToKill);
        if (casualtiesCounter == enemiesToKill)
        {
            Debug.Log("Player won");
            if (OnPlayerWon != null)
                OnPlayerWon();
        }
    }
}
