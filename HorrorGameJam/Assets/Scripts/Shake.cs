﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
[RequireComponent (typeof(Collider))]
public class Shake : MonoBehaviour
{
	[Header("Info")]
	private float _timer;

	[Header("Settings")]
	public float _duration = 1f;
	public float _intensity = 8f;

	Rigidbody rb;
	new Collider collider;


	private void Awake()
	{
		rb = GetComponent<Rigidbody>();
		collider = GetComponent<Collider>();
	}

	public void Begin(float intensity, float duration)
	{
		_intensity = intensity;
		_duration = duration;
		StopAllCoroutines();
		StartCoroutine(Shaking());
		rb.useGravity = true;
		collider.isTrigger = false;
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