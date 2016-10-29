using System.Collections;
using UnityEngine;
using XInputDotNetPure;

namespace Assets.Scripts.Controllers
{
    public class GamepadInputHandler : MonoBehaviour
    {
        public delegate void MovementInputHandler(Vector3 direction);
        public delegate void AttackInputHandler();
        public delegate void StopMovingHandler();
        public delegate void GamepadConnectionHandler();
        public delegate void GamepadDisconnectionHandler();
        public delegate void AButtonHandler();
        public delegate void PauseHandler();
        public delegate void EnterFirstPersonHandler();
        public delegate void ExitFirstPersonHandler();
        public delegate void TakeAimHandler();
        public delegate void ReleaseArrowHandler();

        public event MovementInputHandler OnMovement;
        public event AttackInputHandler OnAttack;
        public event StopMovingHandler OnStopMoving;
        public event GamepadConnectionHandler OnGamepadConnected;
        public event GamepadConnectionHandler OnGamepadDisonnected;
        public event AButtonHandler OnAPressed;
        public event PauseHandler OnPause;
        public event EnterFirstPersonHandler OnEnterFirstPerson;
        public event ExitFirstPersonHandler OnExitFirstPerson;
        public event TakeAimHandler OnTakeAim;
        public event ReleaseArrowHandler OnReleaseArrow;

        private PlayerIndex _playerIndex;
        private GamePadState _currentState;
        private GamePadState _previousState;

        private Vector3 _characterMovement = Vector3.zero;
        private float _joystickDeadZone = 0.1f;
        private float _triggersDeadZone = 0.3f;

        private bool considerInputs;

        private void Start()
        {
            for (int i = 0; i < 4; i++)
            {
                PlayerIndex index = (PlayerIndex) i;
                GamePadState state = GamePad.GetState(index);
                if (state.IsConnected)
                {
                    _playerIndex = index;
                    break;
                }
            }

            considerInputs = false;

        }

        private void OnEnable()
        {
            StartCoroutine(EnableInputs());
        }

        private void OnDisable()
        {
            considerInputs = false;
        }

        private IEnumerator EnableInputs()
        {
            yield return new WaitForSeconds(0.2f);
            considerInputs = true;
        }

        private void Update()
        {
            if (considerInputs)
            {
                _previousState = _currentState;
                _currentState = GamePad.GetState(_playerIndex);

                if (_currentState.IsConnected)
                {
                    HandleMovement();
                    HandleAButton();
                    HandleAttack();
                    HandlePause();
                    HandleFP();
                    HandlerAim();
                }
            }

        }

        private void HandlerAim()
        {
            if (_currentState.Triggers.Right >= _joystickDeadZone && _previousState.Triggers.Right < _joystickDeadZone)
            {
                if (OnTakeAim != null)
                    OnTakeAim();
            }
            else if (_currentState.Triggers.Right < _joystickDeadZone && _previousState.Triggers.Right >= _joystickDeadZone)
            {
                if (OnReleaseArrow != null)
                    OnReleaseArrow();
            }
        }

        private void HandleFP()
        {
            if (_currentState.Triggers.Left >= _joystickDeadZone && _previousState.Triggers.Left < _joystickDeadZone)
            {
                if (OnEnterFirstPerson != null)
                    OnEnterFirstPerson();
            }
            else if (_currentState.Triggers.Left < _joystickDeadZone && _previousState.Triggers.Left >= _joystickDeadZone)
            {

                if (OnExitFirstPerson != null)
                    OnExitFirstPerson();
            }
        }

        private void HandlePause()
        {
            if (_previousState.Buttons.Start == ButtonState.Released &&
                _currentState.Buttons.Start == ButtonState.Pressed)
            {
                if (OnPause != null)
                    OnPause();
            }
        }

        private void HandleConnection()
        {
            if (!_previousState.IsConnected && _currentState.IsConnected)
                if (OnGamepadConnected != null)
                    OnGamepadConnected();
        }

        private void HandleDisconnection()
        {
            if (_previousState.IsConnected && !_currentState.IsConnected)
                if (OnGamepadDisonnected != null)
                    OnGamepadDisonnected();
        }


        private void HandleMovement()
        {
            _characterMovement = Vector3.zero;
            if (Mathf.Abs(_currentState.ThumbSticks.Left.X) > _joystickDeadZone)
                _characterMovement.x = _currentState.ThumbSticks.Left.X;
            if (Mathf.Abs(_currentState.ThumbSticks.Left.Y) > _joystickDeadZone)
                _characterMovement.z = _currentState.ThumbSticks.Left.Y;


            if (_characterMovement != Vector3.zero)
            {
                if (OnMovement != null)
                    OnMovement(_characterMovement);
            }
            else
            {
                if ((Mathf.Abs(_previousState.ThumbSticks.Left.X) >= _joystickDeadZone ||
                     Mathf.Abs(_previousState.ThumbSticks.Left.Y) >= _joystickDeadZone))
                {
                    if (OnStopMoving != null)
                        OnStopMoving();
                }
            }
        }

       

        private void HandleAttack()
        {
            if (_previousState.Buttons.B == ButtonState.Released && _currentState.Buttons.B == ButtonState.Pressed)
                if (OnAttack != null)
                    OnAttack();
        }

        private void HandleAButton()
        {
            if (_previousState.Buttons.A == ButtonState.Released && _currentState.Buttons.A == ButtonState.Pressed)
                if (OnAPressed != null)
                    OnAPressed();
        }
    }
}