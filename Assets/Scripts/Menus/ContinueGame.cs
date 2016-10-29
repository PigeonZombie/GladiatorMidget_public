using UnityEngine;
using System.Collections;

public class ContinueGame : Constants {

    [SerializeField]
    private LoadLevelAsync _levelLoader;

    [SerializeField]
    private OptionsHandler _options;

    //private const string SAVEFILE_PATH = "Assets/Saves/save.dat";
    private GameData data;

    public void Continue()
    {
        data = GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData;
        Debug.Log(data.Level);
        LoadGame();
    }

    private void LoadGame()
    {
        _levelLoader.ClickToLoadAsync(data.Level);
    }
}
