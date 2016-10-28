using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using XInputDotNetPure;

[AddComponentMenu("Camera-Control/Mouse Orbit with zoom")]
public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField] private string[] tagsToIgnoreWithRaycast;

    public Transform targetReference;
    public Transform target;

    private PlayerIndex _playerIndex;
    private GamePadState _currentState;
    private GamepadInputHandler _gamepadInput;

    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;

    public float distanceMin = 3f;
    public float distanceMax = 10f;

    //private Rigidbody rigidbody;
    private float distance = 5.0f;

    float x = 0.0f;
    float y = 0.0f;

    float xSpeed = 2.0f;
    float ySpeed = 2.0f;

    bool canMove;
    private float joystickDeadZone = 0.1f;

    //bool gamepadConnected = false;

    // Use this for initialization
    private void Start()
    {
        _gamepadInput = target.GetComponent<GamepadInputHandler>();
        _gamepadInput.OnGamepadConnected += OnGamepadConnected;

        canMove = true;

        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        //rigidbody = GetComponent<Rigidbody>();

        // Make the rigid body not change rotation
        /*if (rigidbody != null)
        {
            rigidbody.freezeRotation = true;
        }*/

        //string [] names = Input.GetJoystickNames();
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

    private void OnEnable()
    {

        x = transform.rotation.eulerAngles.y;
        y = 0;
    }

    private void FixedUpdate()
    {
        
        if (canMove)
        {
            Vector3 relativePos = transform.position - (targetReference.position);
            RaycastHit hit = new RaycastHit();
            if (Physics.Raycast(targetReference.position, relativePos, out hit, distance + 0.5f))
            {
                bool isClipping = true;

                foreach (string tag in tagsToIgnoreWithRaycast)
                {                   
                    if (hit.collider.tag == tag)
                    {
                        isClipping = false;
                        //distance = hit.distance;
                        break;
                    }
                }
                if(isClipping)
                    distance = hit.distance;
            }
            else
            {
                distance = distanceMax;
            }
        }

    }

    private void LateUpdate()
    {
        _currentState = GamePad.GetState(_playerIndex);
        if (canMove)
        {
            if (distance < distanceMin)
            {
                distance = distanceMin;
            }
            if (distance > distanceMax)
                distance = distanceMax;


            if (targetReference)
            {

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
                y = ClampAngle(y, yMinLimit, yMaxLimit);
                Quaternion rotation = Quaternion.Euler(y, x, 0);
                Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance + 2) + targetReference.position;

                transform.rotation = rotation;
                Quaternion targetRotation = new Quaternion(0.0f, rotation.y, 0.0f, rotation.w);

                target.rotation = targetRotation;
                transform.position = position;
            }
        }

    }

    public static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void StartMoving()
    {
        canMove = true;
    }
    
    public void StopMoving()
    {
        canMove = false;
    }

}
