using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackComponent : MonoBehaviour, IPivotAttack
{

	public AttackCollider Weapon;
	public Transform AttackContainer;
	public float Duration;

	private Coroutine _routine;

	public void Init(IInflictDmg inflictDmg)
	{
		Weapon.Init(inflictDmg, this);
	}

	public void StartAttack()
	{
		Debug.Log("TryAttack");
		if (_routine != null)
			return;

		_routine = StartCoroutine(AttackEnum());
	}

	public void SetDirAttack(Vector2 dir)
	{
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		AttackContainer.rotation = Quaternion.Euler(0f, 0f, angle);
	}

	private IEnumerator AttackEnum()
	{
		Debug.Log("Attack");
		Weapon.SetActiveAttack(true);
		yield return new WaitForSeconds(0.2f);
		Weapon.SetActiveAttack(false);
		_routine = null;
	}

	Quaternion IPivotAttack.GetRotation()
	{
		return AttackContainer.rotation;
	}

	Vector3 IPivotAttack.GetPosition()
	{
		return AttackContainer.position;
	}
}
