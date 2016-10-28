using UnityEngine;
using XInputDotNetPure;

public class GamepadMenuInputHandler : MonoBehaviour
{
    public delegate void SelectHandler();
    public delegate void BackHandler();
    public delegate void ChangeSelectionHandler(float verticalMovement);
    public delegate void ChangeSliderValue(float horizontalMovement);

    public event SelectHandler OnSelect;
    public event BackHandler OnBack;
    public event ChangeSelectionHandler OnChangeSelection;
    public event ChangeSliderValue OnChangeSliderValue;

    private PlayerIndex _playerIndex;
    private GamePadState _currentState;
    private GamePadState _previousState;

    private float _joystickDeadZone = 0.1f;

    private void Start()
    {
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
    }

    private void Update()
    {
        _previousState = _currentState;
        _currentState = GamePad.GetState(_playerIndex);

        HandleSelect();
        HandleBack();
        HandleChangeSelection();
        HandleSliderValueChange();
    }

    private void HandleSliderValueChange()
    {
        float horizontalMovement = _currentState.ThumbSticks.Left.X;

        if (Mathf.Abs(horizontalMovement) >= _joystickDeadZone)
        {
            if (OnChangeSliderValue != null)
                OnChangeSliderValue(horizontalMovement);
        }
    }

    private void HandleSelect()
    {
        if (_previousState.Buttons.A == ButtonState.Released && _currentState.Buttons.A == ButtonState.Pressed)
            if (OnSelect != null)
                OnSelect();
    }

    private void HandleBack()
    {
        if (_previousState.Buttons.B == ButtonState.Released && _currentState.Buttons.B == ButtonState.Pressed)
            if (OnBack != null)
                OnBack();
    }

    private void HandleChangeSelection()
    {
        float verticalMovement = _currentState.ThumbSticks.Left.Y;

        if (Mathf.Abs(verticalMovement) >= _joystickDeadZone)
        {
            if (OnChangeSelection != null)
                OnChangeSelection(verticalMovement);
        }
    }
}
