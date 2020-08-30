using UnityEngine;

public class StepObject : MonoBehaviour
{
    [SerializeField] private GameObject _normalSteps;
    [SerializeField] private GameObject _basementSteps;

    public void SetNormalSteps()
    {
        _basementSteps.SetActive(false);
        _normalSteps.SetActive(true);
    }

    public void SetBasementSteps()
    {
        _normalSteps.SetActive(false);
        _basementSteps.SetActive(true);
    }
}