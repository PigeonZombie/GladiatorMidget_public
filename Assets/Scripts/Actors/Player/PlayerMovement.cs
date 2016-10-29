using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;

public class PlayerMovement : MonoBehaviour {

    [SerializeField]
    private float speed;

    private Rigidbody rb;
    private Vector3 movement;
    private bool canMove;
    private Animator anim;
    private GamepadInputHandler gamepadInput;
    private KeyboardInputHandler keyboardInput;

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    private void Start()
    {
        canMove = true;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        gamepadInput = GetComponent<GamepadInputHandler>();
        gamepadInput.OnMovement += OnMove;
        gamepadInput.OnStopMoving += OnStopMoving;

        keyboardInput = GetComponent<KeyboardInputHandler>();
        keyboardInput.OnMovement += OnMove;
        keyboardInput.OnStopMoving += OnStopMoving;
        

    }

    private void OnDestroy()
    {
        gamepadInput.OnMovement -= OnMove;
        gamepadInput.OnStopMoving -= OnStopMoving;
        keyboardInput.OnMovement -= OnMove;
        keyboardInput.OnStopMoving -= OnStopMoving;
    }


    private void OnMove(Vector3 newMovement)
    {
        
        if (canMove)
        {
            //Debug.Log("Set anim");
            anim.SetBool("Moving", true);

            movement.Set(newMovement.x, 0f, newMovement.z);

            Vector3 rotation = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y,
                transform.rotation.eulerAngles.z);
            Vector3 adjustedMovement = Quaternion.Euler(rotation)*movement;

            movement = adjustedMovement;
            movement = movement*speed*Time.deltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    private void OnStopMoving()
    {
        anim.SetBool("Moving", false);
    }

    


}
