using UnityEngine;

namespace Assets.Scripts.Input.Gameplay
{
    public class KeyboardInputHandler : MonoBehaviour
    {
        public delegate void MovementInputHandler(Vector3 direction);
        public delegate void SkipHandler();
        public delegate void StopMovingHandler();
        public delegate void AttackInputHandler();
        public delegate void SaveHandler();
        public delegate void LoadHandler();
        public delegate void EnterFirstPersonHandler();
        public delegate void ExitFirstPersonHandler();
        public delegate void TakeAimHandler();
        public delegate void ReleaseArrowHandler();

        public event MovementInputHandler OnMovement;
        public event SkipHandler OnSkip;
        public event StopMovingHandler OnStopMoving;
        public event AttackInputHandler OnAttack;
        public event SaveHandler OnSave;
        public event LoadHandler OnLoad;
        public event EnterFirstPersonHandler OnEnterFirstPerson;
        public event ExitFirstPersonHandler OnExitFirstPerson;
        public event TakeAimHandler OnTakeAim;
        public event ReleaseArrowHandler OnReleaseArrow;

        private bool wasMoving = false;
        private bool isInFirstPerson = false;

        private void Update()
        {
            HandleMovement();
            HandleAttack();
            HandleSkip();
            HandleSave();
            HandleLoad();
            HandleFP();
        }


        private void HandleSave()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Escape))
                if (OnSave != null)
                    OnSave();
        }

        private void HandleLoad()
        {
            if(UnityEngine.Input.GetKey(KeyCode.R))
                if (OnLoad != null)
                    OnLoad();
        }

        private void HandleMovement()
        {
            Vector3 translation = Vector3.zero;

            if (UnityEngine.Input.GetKey(KeyCode.A))
            {
                translation += Vector3.left;
            }
            if (UnityEngine.Input.GetKey(KeyCode.D))
            {
                translation += Vector3.right;
            }
            if (UnityEngine.Input.GetKey(KeyCode.W))
            {
                translation += Vector3.forward;
            }
            if (UnityEngine.Input.GetKey(KeyCode.S))
            {
                translation += Vector3.back;
            }

            if (translation != Vector3.zero)
            {
                if (OnMovement != null)
                {
                    OnMovement(translation);
                    wasMoving = true;
                }
            }
            else
            {
                if (OnStopMoving != null && wasMoving)
                {
                    OnStopMoving();
                    wasMoving = false;
                }
            }

        }

        private void HandleSkip()
        {
            if(UnityEngine.Input.GetMouseButtonDown(0))
                if (OnSkip != null)
                    OnSkip();
        }

        private void HandleAttack()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.F))
                if (OnAttack != null)
                    OnAttack();
        }

        private void HandleFP()
        {
            if (UnityEngine.Input.GetMouseButtonDown(0))
            {
                if (!isInFirstPerson)
                {
                    if (OnEnterFirstPerson != null)
                    {
                        OnEnterFirstPerson();
                        isInFirstPerson = true;
                    }
                }
                /*else
                {
                    if(OnExitFirstPerson!=null)
                    {
                        OnExitFirstPerson();
                        isInFirstPerson = false;
                    }
                }*/
            }
            /*else if (UnityEngine.Inpu)
            {
                if (OnExitFirstPerson != null)
                    OnExitFirstPerson();
            }*/
        }
    }
}