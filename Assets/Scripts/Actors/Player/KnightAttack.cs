using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;

public class KnightAttack : MonoBehaviour
{

    private GamepadInputHandler _gamepadInput;
    private KeyboardInputHandler _keyboardInput;
    private Animator anim;
    private PlayerMovement _playerMovement;

    private void Start()
    {
        _gamepadInput = GetComponentInParent<GamepadInputHandler>();
        _gamepadInput.OnAttack += OnAttack;
        _keyboardInput = GetComponentInParent<KeyboardInputHandler>();
        _keyboardInput.OnAttack += OnAttack;

        _playerMovement = GetComponentInParent<PlayerMovement>();

        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            _playerMovement.CanMove = true;
        }
        else
        {
            _playerMovement.CanMove = false;
        }

    }

    private void OnDestroy()
    {
        _gamepadInput.OnAttack -= OnAttack;
        _keyboardInput.OnAttack -= OnAttack;
    }
	
    private void OnAttack()
    {
	    anim.SetTrigger("Attack");
    }


}
