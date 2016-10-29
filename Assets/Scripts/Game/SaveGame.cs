using UnityEngine;
using System.Collections;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine.SceneManagement;

public class SaveGame : Constants {

    //private const string SAVEFILE_PATH = "Assets/Saves/save.dat";
    [SerializeField]
    private OptionsHandler _options;

    private GameData _dataToSave;

    public void Save()
    {
        _dataToSave = GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData;
        if (_options != null)
        {
            _dataToSave.masterVolume = _options.MasterVolume;
            _dataToSave.SFXvolume = _options.SFXVolume;
        }
        StartCoroutine(SaveInFile());
    }

	private IEnumerator SaveInFile()
	{
        Debug.Log("Level saved: "+_dataToSave.Level);
        BinaryFormatter formatter = new BinaryFormatter();

        using (FileStream fs = File.Open(SAVEFILE_PATH,FileMode.Open))
        {
            //Debug.Log("Save:" + _dataToSave.masterVolume + "," + _dataToSave.SFXvolume);
            formatter.Serialize(fs, _dataToSave);
            fs.Close();
            yield return null;
        }
        //Debug.Log("saved");
    }
}
