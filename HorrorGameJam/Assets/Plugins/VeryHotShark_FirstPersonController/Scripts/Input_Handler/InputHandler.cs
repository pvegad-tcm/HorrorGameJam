using UnityEngine;
using NaughtyAttributes;

namespace VHS
{    
    public class InputHandler : MonoBehaviour
    {
        public bool JumpAllowed;
        
        [Space,Header("Input Data")]
        [SerializeField] private CameraInputData cameraInputData = null;
        [SerializeField] private MovementInputData movementInputData = null;
        [SerializeField] private InteractionInputData interactionInputData = null;

        [Space, Header("Sound Objects")] 
        [SerializeField] private GameObject stepsObject;
        
        void Start()
        {
            cameraInputData.ResetInput();
            movementInputData.ResetInput();
            interactionInputData.ResetInput();
        }

        void Update()
        {
            GetCameraInput();
            GetMovementInputData();
            GetInteractionInputData();
            SetSoundObjects();
        }

        void GetInteractionInputData()
        {
            interactionInputData.InteractedClicked = Input.GetKeyDown(KeyCode.E);
            interactionInputData.InteractedReleased = Input.GetKeyUp(KeyCode.E);
        }

        void GetCameraInput()
        {
            cameraInputData.InputVectorX = Input.GetAxis("Mouse X");
            cameraInputData.InputVectorY = Input.GetAxis("Mouse Y");

            cameraInputData.ZoomClicked = Input.GetMouseButtonDown(1);
            cameraInputData.ZoomReleased = Input.GetMouseButtonUp(1);
        }

        void GetMovementInputData()
        {
            movementInputData.InputVectorX = Input.GetAxisRaw("Horizontal");
            movementInputData.InputVectorY = Input.GetAxisRaw("Vertical");

            movementInputData.RunClicked = Input.GetKeyDown(KeyCode.LeftShift);
            movementInputData.RunReleased = Input.GetKeyUp(KeyCode.LeftShift);

            if(movementInputData.RunClicked)
                movementInputData.IsRunning = true;

            if(movementInputData.RunReleased)
                movementInputData.IsRunning = false;

            if (JumpAllowed)
            {
                movementInputData.JumpClicked = Input.GetKeyDown(KeyCode.Space);
            }
            movementInputData.CrouchClicked = Input.GetKeyDown(KeyCode.LeftControl);
        }
        
        void SetSoundObjects()
        {
            stepsObject.SetActive(movementInputData.HasInput);
        }
    }
}