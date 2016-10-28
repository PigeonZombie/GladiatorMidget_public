using UnityEngine;
using System.Collections;

public class OptionsLoader : MonoBehaviour
{
    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource[] _sfxSources;
    [SerializeField]
    private OptionsHandler _options;

    private GameData _data;

	private void Start ()
	{
	    _data = GameObject.FindGameObjectWithTag("GameData").GetComponent<LoadGame>().GameData;
	    if (_data.Level != -1)
	    {
	        _backgroundMusic.volume = _data.masterVolume;

	        foreach (AudioSource sfx in _sfxSources)
	        {
	            sfx.volume = _data.SFXvolume;
	        }

	        if (_options != null)
	        {
	            _options.MasterVolume = _data.masterVolume;
	            _options.SFXVolume = _data.SFXvolume;
	        }
	    }
	    else
	    {
            _backgroundMusic.volume = 0.5f;

            foreach (AudioSource sfx in _sfxSources)
            {
                sfx.volume = 0.5f;
            }

            if (_options != null)
            {
                _options.MasterVolume = 0.5f;
                _options.SFXVolume = 0.5f;
            }
        }
	}
	

}
