using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(BoxCollider))]
public class Shake : MonoBehaviour
{
	[Header("Info")]
	private float _timer;

	[Header("Settings")]
	public float _duration = 1f;
	public float _intensity = 8f;

	Rigidbody rb;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
	}

	public void Begin(float intensity, float duration)
	{
		print("begin");
		_intensity = intensity;
		_duration = duration;
		StopAllCoroutines();
		StartCoroutine(Shaking());
	}

	private IEnumerator Shaking()
	{
		_timer = 0f;
		
		while (_timer < _duration)
		{
			_timer += Time.deltaTime;

			Vector3 forceDirector = Random.insideUnitSphere * _intensity;

			//transform.position = _randomPos;
			rb.AddForce(forceDirector);

			yield return null;
		}
	}

}