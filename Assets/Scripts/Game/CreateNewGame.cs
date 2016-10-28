using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class CreateNewGame : MonoBehaviour {

    [SerializeField]
    private LoadLevelAsync _levelLoader;

    [SerializeField]
    private OptionsHandler _options;

    private const string SAVEFILE_PATH = "/save.dat";
    private GameData startingData;
 

    public void CreateGame()
    {
        startingData = GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData;
        StartCoroutine(Create());
    }
	
    /// <summary>
    /// Create or overwrite the savefile
    /// </summary>
    /// <returns></returns>
	public IEnumerator Create()
    {
        
        startingData.Level = 1;
        startingData.masterVolume = _options.MasterVolume;
        startingData.SFXvolume = _options.SFXVolume;

        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = File.Create(Application.persistentDataPath + SAVEFILE_PATH))
        {
            formatter.Serialize(fs, startingData);
            yield return null;
        }

        _levelLoader.ClickToLoadAsync(startingData.Level);
    }

}
