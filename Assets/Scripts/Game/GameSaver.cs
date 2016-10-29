using System;
using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;
using UnityEngine.SceneManagement;

public class GameSaver : Constants
{
    [SerializeField] private OptionsHandler _options;

    //private string SAVEFILE_PATH = "/save.dat";
    private GameData data;
    private KeyboardInputHandler _keyboardInput;

    public GameData Data
    {
        get { return data; }
    }

    private void Start()
    {
        data = new GameData();
        DontDestroyOnLoad(gameObject);
    }


    public void Save()
    {
        StartCoroutine(SaveGame());
    }

    public void Load()
    {
        StartCoroutine(LoadGame());
        
    }

    private IEnumerator SaveGame()
    {
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = File.Create(SAVEFILE_PATH))
        {
            data.Level = SceneManager.GetActiveScene().buildIndex;
            data.masterVolume = _options.MasterVolume;
            data.SFXvolume = _options.SFXVolume;
            formatter.Serialize(fs, data);
            yield return null;
        }
    }

    private IEnumerator LoadGame()
    {
        
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(SAVEFILE_PATH))
        {
            using (FileStream fs = File.Open(SAVEFILE_PATH, FileMode.OpenOrCreate))
            {
                data = (GameData) formatter.Deserialize(fs);
                yield return null;
            }
        }

        //GameObject.FindGameObjectWithTag("LevelLoader").GetComponent<LoadLevelAsync>().ClickToLoadAsync(data.Level-1);
        /*PlayerHealth _player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerHealth>();
        if (_player != null)
            _player.DeathCounter = Data.DeathCounter;*/
    }

    public void Delete()
    {
        File.Delete(SAVEFILE_PATH);
    }

}