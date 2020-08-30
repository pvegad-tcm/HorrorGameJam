using UnityEngine;
using VHS;

public class PlayerControllerMaster : MonoBehaviour
{
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private InteractionController _interactionController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private InputHandler _inputHandler;

    [Space, SerializeField] private string _pushableTag = "Pushable";
    [SerializeField, Range(0, 250)] private float _force = 100;
    
    [Space, SerializeField] private StepObject _stepObject;
    [SerializeField] private string _normalStepsTriggerTag = "NormalStepsTrigger";
    [SerializeField] private string _basementStepsTriggerTag = "BasementStepsTrigger";
    
    private bool _disabled = false;

    public bool Disabled => _disabled;

    public void Enable()
    {
        if (!_disabled) return;
        
        _firstPersonController.enabled = true;
        _interactionController.enabled = true;
        _cameraController.enabled = true;
        _inputHandler.enabled = true;
            
        _disabled = false;
    }

    public void Disable()
    {
        if (_disabled) return;
        
        _firstPersonController.enabled = false;
        _interactionController.enabled = false;
        _cameraController.enabled = false;
        _inputHandler.enabled = false;
            
        _disabled = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag(_normalStepsTriggerTag))
        {
            _stepObject.SetNormalSteps();
        }
        
        if (hit.gameObject.CompareTag(_basementStepsTriggerTag))
        {
            _stepObject.SetBasementSteps();
        }
        
        if (!hit.gameObject.CompareTag(_pushableTag)) return;

        var dir = hit.point - transform.position;
        dir = -dir.normalized;
        hit.rigidbody.AddForce(dir * _force);
    }
}