using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class LoadGame : Constants {

    //private const string SAVEFILE_PATH = "Assets/Saves/save.dat";
    public GameData GameData;

    /*public GameData GameData
    {
        get { return gameData; }
    }*/

	private void Start()
	{
	    Time.timeScale = 1;
        StartCoroutine(LoadSaveFile());
    }

	private IEnumerator LoadSaveFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        if (File.Exists(SAVEFILE_PATH))
        {
            using (FileStream fs = File.Open(SAVEFILE_PATH, FileMode.Open))
            {
                GameData = (GameData)formatter.Deserialize(fs);
                fs.Close();
                yield return null;
            }
        }
        else
        {
            GameData.Level = -1;
        }

    }
}
