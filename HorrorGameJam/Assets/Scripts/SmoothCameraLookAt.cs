using UnityEngine;
using VHS;

public class SmoothCameraLookAt : MonoBehaviour
{
    [SerializeField] private bool _rotateInY;
    [SerializeField] private PlayerControllerMaster _playerControllerMaster;
    [SerializeField] private CameraBreathing _cameraBreathing;
    [SerializeField] private GameObject _stepsGameObject;
    [SerializeField] private float endTimer;


   [Space, SerializeField] private Transform _target;
    [SerializeField, Range(0, 2)] private float _speed = 1;

    private void OnEnable()
    {
        _playerControllerMaster.Disable();
        _cameraBreathing.enabled = false;
        _stepsGameObject.SetActive(false);

        Invoke("DetachCamera", endTimer);

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

    private void DetachCamera()
    {
        Transform child = transform.GetChild(0);
        child.GetComponent<Camera>().enabled = true;
        //child.gameObject.SetActive(true);
        child.parent = transform.parent;
        child.rotation = transform.rotation;
   
        gameObject.SetActive(false);
    }
}