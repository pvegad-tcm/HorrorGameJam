using UnityEngine;
using VHS;

public class PlayerControllerMaster : MonoBehaviour
{
    [SerializeField] private FirstPersonController _firstPersonController;
    [SerializeField] private InteractionController _interactionController;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private InputHandler _inputHandler;
    
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
}
