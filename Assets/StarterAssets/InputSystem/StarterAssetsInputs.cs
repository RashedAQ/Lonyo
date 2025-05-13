using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
#endif

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour, IDataPersistence
    {
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;

        [Header("Movement Settings")]
        public bool analogMovement;
        public bool playerMoveEnabled = true;

        [Header("Mouse Cursor Settings")]
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;
        public bool use;
        [Header("Spawn Settings")]
        [SerializeField] private Transform spawnPoint; // Assign this in the Inspector
        [SerializeField] private Camera playerCamera;
        private void Awake()
        {
            SetCursorState(true);

            EnablePlayerMovement(true);




        }
        public void OnUse(InputValue value)
        {
            use = value.isPressed;
        }
        private void Update()
        {
            use = false;

        }

#if ENABLE_INPUT_SYSTEM
        public void OnMove(InputValue value)
        {
            if (playerMoveEnabled)
                MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
                LookInput(value.Get<Vector2>());
        }

        public void OnJump(InputValue value) => JumpInput(value.isPressed);
        public void OnSprint(InputValue value) => SprintInput(value.isPressed);

        public void OnEscape(InputValue value)
        {
            if (value.isPressed)
                ToggleCursorLock();
        }
#endif

        public void MoveInput(Vector2 newMoveDirection) => move = newMoveDirection;
        public void LookInput(Vector2 newLookDirection) => look = newLookDirection;
        public void JumpInput(bool newJumpState) => jump = newJumpState;
        public void SprintInput(bool newSprintState) => sprint = newSprintState;

        public void EnablePlayerMovement(bool enable)
        {
            playerMoveEnabled = enable;
            if (!enable)
            {
                move = Vector2.zero;
                look = Vector2.zero;
            }
        }

        public void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
            Cursor.visible = !newState;
        }

        public void ToggleCursorLock()
        {
            cursorLocked = !cursorLocked;
            cursorInputForLook = cursorLocked;
            SetCursorState(cursorLocked);

            if (!cursorLocked)
                look = Vector2.zero;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }
        public void LoadData(GameData data)
        {
            if (data._isNewGame)
            {
                // If it's a new game, use the spawn point position
                if (spawnPoint != null)
                {
                    this.transform.position = spawnPoint.position;
                    this.transform.rotation = spawnPoint.rotation;

                    if (playerCamera != null)
                    {
                        // Reset camera to default rotation
                        playerCamera.transform.localRotation = Quaternion.identity;
                    }
                }
            }
            else
            {
                // If continuing a saved game, use the saved position
                this.transform.position = data._playerPos;
                this.transform.rotation = data._playerRota;

                if (playerCamera != null)
                {
                    playerCamera.transform.localRotation = data._cameraRota;
                }
            }

            // Reset input look direction
            look = Vector2.zero;
        }
        public void SaveData(ref GameData data)
        {
            // Only save position if we're not in a new game
            if (!data._isNewGame)
            {
                data._playerPos = this.transform.position;
                data._playerRota = this.transform.rotation;

                if (playerCamera != null)
                {
                    data._cameraRota = playerCamera.transform.localRotation;
                }
            }

            // After saving the first time, it's no longer a new game
            data._isNewGame = false;
        }
    
}
    
}






