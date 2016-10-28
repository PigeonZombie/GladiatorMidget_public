using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionsHandler : MonoBehaviour {

    [SerializeField]
    private AudioSource _backgroundMusic;
    [SerializeField]
    private AudioSource[] _sfxSources;
    [SerializeField]
    private Slider _mainVolumeSlider;
    [SerializeField]
    private Slider _sfxVolumeSlider;

    public float MasterVolume
    {
        get { return _mainVolumeSlider.value; }
        set
        {
            _backgroundMusic.volume = value;
            _mainVolumeSlider.value = value;
        }
    }

    public float SFXVolume
    {
        get { return _sfxVolumeSlider.value; }
        set
        {
            foreach (AudioSource sfx in _sfxSources)
            {
                sfx.volume = value;
            }
            _sfxVolumeSlider.value = value;
        }
    }

    private void Start()
    {
        _backgroundMusic.volume = _mainVolumeSlider.value;
        foreach (AudioSource _source in _sfxSources)
        {
            _source.volume = _sfxVolumeSlider.value;
        }
    }

    public void OnMusicVolumeChanged()
    {
        _backgroundMusic.volume = _mainVolumeSlider.value;

    }

    public void OnSFXVolumeChanged()
    {
        foreach (AudioSource _source in _sfxSources)
        {
            _source.volume = _sfxVolumeSlider.value;
        }

    }
}
