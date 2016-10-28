using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;

public class PauseMenuHandler : MonoBehaviour {

    [SerializeField]
    private GameObject _pausePanel;

    private GamepadInputHandler _playerInput;
    private KeyboardInputHandler _playerKeyboardInput;

    private void Start()
    {
        _playerInput = GameObject.FindGameObjectWithTag("Player").GetComponent<GamepadInputHandler>();
        _playerKeyboardInput = GameObject.FindGameObjectWithTag("Player").GetComponent<KeyboardInputHandler>();
        _playerInput.OnPause += OpenCloseMenu;
    }

    private void OnDestroy()
    {
        _playerInput.OnPause -= OpenCloseMenu;
    }

    public void OpenCloseMenu()
    {
        _pausePanel.gameObject.SetActive(!_pausePanel.gameObject.activeInHierarchy);
        if (_pausePanel.gameObject.activeInHierarchy)
        {
            Time.timeScale = 0;
            _playerInput.enabled = false;
            //_playerKeyboardInput.enabled = false;
        }
        else
        {
            _playerInput.enabled = true;
            Time.timeScale = 1;
            //_playerKeyboardInput.enabled = true;
        }
    }
}
