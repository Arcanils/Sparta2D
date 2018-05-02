using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
	public float OffsetY;


	private Transform _transCam;

	public void Awake()
	{
		_transCam = transform;
	}

	public void Reset()
	{
		OffsetY = 4;
	}

	public void UpdatePosCam()
	{
		//NONE
	}

	public void UpdateFollowingCam()
	{
		float Center = 0f;

		_transCam.position = new Vector3(_transCam.position.x, Center, _transCam.position.z);
	}
}
