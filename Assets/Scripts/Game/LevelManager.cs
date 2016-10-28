using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{

    private PlayerHealth _playerHealth;
    private EnemyManager _enemyManager;
    private GameData _data;
    private SaveGame _saveGame;
    private LoadLevelAsync _levelLoader;

    private void Start()
    {
        _playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        _playerHealth.OnPlayerDeath += OnPlayerDeath;

        _enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        _enemyManager.OnPlayerWon += HandleVictory;

        _levelLoader = GetComponent<LoadLevelAsync>();

        _saveGame = GetComponent<SaveGame>();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData.Level=SceneManager.GetActiveScene().buildIndex-1;
        _data.Level = SceneManager.GetActiveScene().buildIndex - 1;
        _saveGame.Save();
        GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData.Level = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnDestroy()
    {
        _playerHealth.OnPlayerDeath -= OnPlayerDeath;
        _enemyManager.OnPlayerWon -= HandleVictory;
    }

    private void OnPlayerDeath()
    {
        _saveGame.Save();
        GameObject.FindGameObjectWithTag("GameManager").GetComponent<LoadLevelAsync>().ClickToLoadAsync(SceneManager.GetActiveScene().buildIndex);
    }

    private void HandleVictory()
    {
        _data.Level = SceneManager.GetActiveScene().buildIndex + 1;
        _saveGame.Save();
        _levelLoader.ClickToLoadAsync(_data.Level);
    }
}
