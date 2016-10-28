using Assets.Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;
using XInputDotNetPure;

public class GamepadSelection : MonoBehaviour {

    [SerializeField]
    private Text[] buttons;

    [SerializeField] 
    private Color hoveredColor;

    private int selectedButtonIndex = 0;
    private float changeSelectionDelay = 0.2f;
    private float nextSelectionChange = 0;
    private GamepadInputHandler _gamepadInput;
    private float deadZone = 0.1f;

	private void Start ()
	{
	    _gamepadInput = GetComponent<GamepadInputHandler>();
	    _gamepadInput.OnMovement += HandleGamepadSelection;
	    _gamepadInput.OnAPressed += OnSelect;

	    PlayerIndex _playerIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            PlayerIndex index = (PlayerIndex)i;
            GamePadState state = GamePad.GetState(index);
            if (state.IsConnected)
            {
                _playerIndex = index;
                break;
            }
        }

	    if (!GamePad.GetState(_playerIndex).IsConnected)
	        enabled = false;

        /*for(int i=0; i< buttons.Length;i++)
            buttons[i].rectTransform.parent.GetComponent<Button>().onClick.AddListener();*/
	}

    private void OnDestroy()
    {
        _gamepadInput.OnMovement -= HandleGamepadSelection;
        _gamepadInput.OnAPressed -= OnSelect;
    }

    private void Update()
    {
    }

    private void HandleGamepadSelection(Vector3 movement)
    {
        if (Mathf.Abs(movement.z) > deadZone && Time.time >= nextSelectionChange)
        {
            nextSelectionChange = Time.time + changeSelectionDelay;
            Debug.Log(movement);
            buttons[selectedButtonIndex].color = Color.white;

            if (movement.z >= deadZone)
            {              
                if (selectedButtonIndex > 0)
                    selectedButtonIndex--;
                else
                    selectedButtonIndex = buttons.Length-1;
            }
            else if (movement.z <= -deadZone)
            {
                if (selectedButtonIndex == buttons.Length-1)
                    selectedButtonIndex = 0;
                else
                    selectedButtonIndex++;
            }

            buttons[selectedButtonIndex].color = hoveredColor;
        }
    }

    private void OnSelect()
    {
        
    }

}
