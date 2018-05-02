using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIEntityStatus : MonoBehaviour {

	public Transform UIMaskHP;
	public float DistanceToMove = 1.5f;

	public void UpdateHPValue(float newValue)
	{
		UIMaskHP.localPosition = new Vector3(-(1f - newValue) * DistanceToMove, 0f);
	}

	public void OnDeath()
	{

	}
}
