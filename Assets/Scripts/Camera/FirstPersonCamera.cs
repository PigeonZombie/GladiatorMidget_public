using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using XInputDotNetPure;

public class FirstPersonCamera : MonoBehaviour {

    //public Transform gun;
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject camRef;

    private float x = 0.0f;
    private float y = 0.0f;

    private float xSpeed = 3.0f;
    private float ySpeed = 3.0f;

    private float yMinLimit = -20f;
    private float yMaxLimit = 30f;

    private float xMinLimit = -85f;
    private float xMaxLimit = 360f;

    private float zoomSpeed = 20f;
    private float minZoomFOV = 10f;
    private float maxZoomFOV = 60f;

    bool canMove;
    private float joystickDeadZone = 0.1f;

    //Camera gunCam;

    private PlayerIndex _playerIndex;
    private GamePadState _currentState;
    private GamepadInputHandler _gamepadInput;

    private void Start ()
	{
	    _gamepadInput = player.GetComponent<GamepadInputHandler>();
        _gamepadInput.OnGamepadConnected += OnGamepadConnected;

        canMove = true;


        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;
        transform.position = camRef.transform.position;
        // gunCam = GetComponent<Camera>();

        for (int i = 0; i < 4; i++)
        {
            PlayerIndex index = (PlayerIndex)i;
            GamePadState state = GamePad.GetState(index);
            if (state.IsConnected)
            {
                _playerIndex = index;
                //gamepadConnected = true;
                break;
            }
        }
    }

    private void OnGamepadConnected()
    {
        for (int i = 0; i < 4; i++)
        {
            PlayerIndex index = (PlayerIndex)i;
            GamePadState state = GamePad.GetState(index);
            if (state.IsConnected)
            {
                _playerIndex = index;
                //gamepadConnected = true;
                break;
            }
        }
    }


    private void Update () 
    {
        _currentState = GamePad.GetState(_playerIndex);

        if (_currentState.IsConnected)
        {
            _currentState = GamePad.GetState(_playerIndex);
            float xInput = _currentState.ThumbSticks.Right.X;
            float yInput = _currentState.ThumbSticks.Right.Y;
            if (Mathf.Abs(xInput) >= joystickDeadZone)
                x += xInput * xSpeed;
            if (Mathf.Abs(yInput) >= joystickDeadZone)
                y += yInput;
        }
        else
        {
            x += Input.GetAxis("Mouse X") * xSpeed;
            y -= Input.GetAxis("Mouse Y") * ySpeed;
        }


        Quaternion rotation = Quaternion.Euler(y, x, 0);
        transform.rotation = rotation;
        
        Quaternion targetRotation = new Quaternion(0.0f, rotation.y, 0.0f, rotation.w);

        player.transform.rotation = targetRotation;

    }

    private void OnEnable()
    {
        x = transform.rotation.x;

        transform.SetParent(player.transform);
        transform.position = camRef.transform.position;

        transform.rotation.Set(x, transform.parent.rotation.y, transform.parent.rotation.z, transform.parent.rotation.w);

        x = transform.rotation.eulerAngles.y;
        y = transform.rotation.eulerAngles.x;
    }

    private void OnDisable()
    {

        transform.SetParent(null);
    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }


}
