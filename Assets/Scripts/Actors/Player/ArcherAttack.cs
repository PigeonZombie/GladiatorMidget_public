using UnityEngine;
using System.Collections;
using Assets.Scripts.Controllers;
using Assets.Scripts.Input.Gameplay;

public class ArcherAttack : MonoBehaviour
{
    [SerializeField]
    private int arrowSpeed;
    [SerializeField]
    private ThirdPersonCamera _thirdPersonCamera;
    [SerializeField]
    private FirstPersonCamera _firstPersonCamera;
    [SerializeField]
    private SkinnedMeshRenderer _renderer;
    [SerializeField]
    private GetInShootingPosition _bowPositionHandler;
    [SerializeField]
    private GameObject _arrow;
    [SerializeField]
    private GameObject _arrowPrefab;

    [SerializeField]
    private GameObject _sword;
   

    private GamepadInputHandler _gamepadInput;
    private KeyboardInputHandler _keyboardInput;
    private Animator anim;
    private PlayerMovement _playerMovement;

    

    private void Start()
    {
        _gamepadInput = GetComponentInParent<GamepadInputHandler>();
        _gamepadInput.OnAttack += OnAttack;
        _gamepadInput.OnEnterFirstPerson += EnterFP;
        _gamepadInput.OnExitFirstPerson += ExitFP;
        _gamepadInput.OnTakeAim += TakeAim;
        _gamepadInput.OnReleaseArrow += Shoot;

        _keyboardInput = GetComponentInParent<KeyboardInputHandler>();
        _keyboardInput.OnAttack += OnAttack;
        _keyboardInput.OnEnterFirstPerson += EnterFP;
        _keyboardInput.OnExitFirstPerson += ExitFP;

        _playerMovement = GetComponentInParent<PlayerMovement>();

        anim = GetComponentInParent<Animator>();
    }

    private void Update()
    {
        if (!anim.GetCurrentAnimatorStateInfo(0).IsName("Attack1"))
        {
            //Debug.Log("canMove");
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
        _gamepadInput.OnEnterFirstPerson -= EnterFP;
        _gamepadInput.OnExitFirstPerson -= ExitFP;
        _gamepadInput.OnTakeAim -= TakeAim;
        _gamepadInput.OnReleaseArrow -= Shoot;

        _keyboardInput.OnAttack -= OnAttack;
        _keyboardInput.OnEnterFirstPerson += EnterFP;
        _keyboardInput.OnExitFirstPerson += ExitFP;
    }

    private void TakeAim()
    {
        _arrow.transform.Translate(2,-0.1f,0);
        _arrow.GetComponent<MeshRenderer>().enabled = true;
    }

    private void Shoot()
    {
        GameObject newArrow = (GameObject)Instantiate(_arrowPrefab, _arrow.transform.position, _arrow.transform.rotation, null);
        newArrow.GetComponent<Rigidbody>().AddRelativeForce(Vector3.left*arrowSpeed);
        //newArrow.transform.Translate(-2,0.1f,0);

        _arrow.transform.Translate(-2, 0.1f, 0);
        //_arrow.GetComponent<MeshRenderer>().enabled = false;
    }

    private void OnAttack()
    {
        anim.SetTrigger("Attack");
    }

    private void EnterFP()
    {
        _renderer.enabled = false;
        _firstPersonCamera.enabled = true;
        _thirdPersonCamera.enabled = false;
        _bowPositionHandler.GetInPosition();
        _arrow.GetComponent<MeshRenderer>().enabled = true;
        _sword.SetActive(false);
    }

    private void ExitFP()
    {
        _renderer.enabled = true;
        _firstPersonCamera.enabled = false;
        _thirdPersonCamera.enabled = true;
        _bowPositionHandler.GetBackInPosition();
        _arrow.GetComponent<MeshRenderer>().enabled = false;
        _sword.SetActive(true);
    }
}
