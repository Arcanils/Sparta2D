using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public float OffsetY;


	private Transform _transCam;
	private Transform _target;

	public void Awake()
	{
		_transCam = transform;
	}

	public void Reset()
	{
		OffsetY = 4;
	}

	public void Update()
	{
		UpdateFollowingCam();
	}

	public void UpdatePosCam()
	{
		//NONE
	}

	public void UpdateFollowingCam()
	{
		if (_target == null)
			return;

		Vector2 center = new Vector2(_target.position.x, _target.position.y);
		_transCam.position = new Vector3(center.x, center.y, _transCam.position.z);
	}

	public void Init(Transform target)
	{
		_target = target;
	}
}
