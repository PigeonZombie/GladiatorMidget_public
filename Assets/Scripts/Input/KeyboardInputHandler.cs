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

        public event MovementInputHandler OnMovement;
        public event SkipHandler OnSkip;
        public event StopMovingHandler OnStopMoving;
        public event AttackInputHandler OnAttack;
        public event SaveHandler OnSave;
        public event LoadHandler OnLoad;

        private void Update()
        {
            HandleMovement();
            HandleAttack();
            HandleSkip();
            HandleSave();
            HandleLoad();
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
                    OnMovement(translation);
            }
            else
            {
                if (OnStopMoving != null)
                    OnStopMoving();
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
    }
}