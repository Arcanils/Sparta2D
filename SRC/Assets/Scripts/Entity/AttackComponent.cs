using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour {

	public AttackCollider Weapon;
	public Transform AttackContainer;
	public float Duration;

	private Coroutine _routine;

	public void Init(IInflictDmg inflictDmg)
	{
		Weapon.Init(inflictDmg);
	}

	public void StartAttack()
	{
		Debug.Log("TryAttack");
		if (_routine != null)
			return;

		_routine = StartCoroutine(AttackEnum());
	}

	private IEnumerator AttackEnum()
	{
		Debug.Log("Attack");
		Weapon.SetActiveAttack(true);
		yield return new WaitForSeconds(0.2f);
		Weapon.SetActiveAttack(false);
		_routine = null;
	}
}
