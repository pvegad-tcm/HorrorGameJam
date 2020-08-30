using UnityEngine;
using VHS;

public class SmoothCameraLookAt : MonoBehaviour
{
    [SerializeField] private bool _rotateInY;
    [SerializeField] private PlayerControllerMaster _playerControllerMaster;
    [SerializeField] private CameraBreathing _cameraBreathing;
    
    [Space, SerializeField] private Transform _target;
    [SerializeField, Range(0, 2)] private float _speed = 1;

    private void OnEnable()
    {
        _playerControllerMaster.Disable();
        _cameraBreathing.enabled = false;
    }
    
    private void OnDisable()
    {
        _cameraBreathing.enabled = true;
        _playerControllerMaster.Enable();
    }

    private void Update()
    {

        if (_rotateInY)
        {
            var targetDir = _target.position - transform.position;
            targetDir.y = 0.0f;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(targetDir), Time.time * _speed);
        }
        else
        {
            var originalRotation = transform.rotation;
            transform.LookAt(_target);
        
            var newRotation = transform.rotation;
            transform.rotation = originalRotation;
            transform.rotation = Quaternion.Lerp(transform.rotation, newRotation, _speed * Time.deltaTime);

        }
    }
}