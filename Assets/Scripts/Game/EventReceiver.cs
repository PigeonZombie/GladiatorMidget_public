using UnityEngine;
using System.Collections;

public class EventReceiver : MonoBehaviour {

    [SerializeField]
    private EntranceTrigger _entranceTrigger;
    [SerializeField]
    private GameObject enemyManager;
    [SerializeField]
    private EnemyManager _manager;

	private void Start () {
        if (_entranceTrigger != null)
            _entranceTrigger.OnPlayerEnteredArena += PlayerEnteredArena;

        if(_entranceTrigger!=null)
            _manager.OnPlayerWon += PlayerWon;
	}


    private void OnDestroy()
    {
        _entranceTrigger.OnPlayerEnteredArena -= PlayerEnteredArena;
        _manager.OnPlayerWon -= PlayerWon;
    }
	

    private void PlayerEnteredArena()
    {
        Debug.Log("Event called");
        if (enemyManager != null)
            enemyManager.GetComponent<EnemyManager>().enabled=true;
    }

    private void PlayerWon()
    {
        gameObject.SetActive(false);
    }
}
