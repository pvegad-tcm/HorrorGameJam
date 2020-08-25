using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class Shake : MonoBehaviour
{
	[Header("Info")]
	private Vector3 _startPos;
	private float _timer;
	private Vector3 _randomPos;

	[Header("Settings")]
	[Range(0f, 2f)]
	public float _time = 0.2f;

	public float _impulse = 0.1f;
	[Range(0f, 0.1f)]
	public float _delayBetweenShakes = 0f;

	Rigidbody rb;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		_startPos = transform.position;
		Begin();
	}

	private void OnValidate()
	{
		if (_delayBetweenShakes > _time)
			_delayBetweenShakes = _time;
	}

	public void Begin()
	{
		StopAllCoroutines();
		StartCoroutine(Shaking());
	}

	private IEnumerator Shaking()
	{
		_timer = 0f;
		
		while (true)
		{
			_timer += Time.deltaTime;

			Vector3 forceDirector = Random.insideUnitSphere * _impulse;

			//transform.position = _randomPos;
			rb.AddForce(forceDirector);

			if (_delayBetweenShakes > 0f)
			{
				yield return new WaitForSeconds(_delayBetweenShakes);
			}
			else
			{
				yield return null;
			}
		}

		transform.position = _startPos;
	}

}