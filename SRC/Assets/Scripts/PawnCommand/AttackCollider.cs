using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

	public Rect RectCollision;

	public float Damage;

	private IPivotAttack _pivotAttack;
	private Coroutine _routine;
	private Transform _trans;
	private Vector3 _orgine;
	
	private void OnDrawGizmosSelected()
	{
		Gizmos.DrawCube(transform.position + new Vector3(RectCollision.x, RectCollision.y), Vector3.one * 0.1f);
		Gizmos.DrawCube(transform.position + new Vector3(RectCollision.x + RectCollision.size.x, RectCollision.y), Vector3.one * 0.1f);
		Gizmos.DrawCube(transform.position + new Vector3(RectCollision.x, RectCollision.y + RectCollision.size.y), Vector3.one * 0.1f);
		Gizmos.DrawCube(transform.position + new Vector3(RectCollision.x + RectCollision.size.x, RectCollision.y + RectCollision.size.y), Vector3.one * 0.1f);
	}

	public void Init(IPivotAttack pivotAttack)
	{
		_trans = transform;
		_pivotAttack = pivotAttack;
		_orgine = _trans.position - pivotAttack.GetPosition();
	}

	public void SetActiveAttack(bool enable)
	{
		if (_routine != null)
			StopCoroutine(_routine);
		if (enable)
			_routine = StartCoroutine(TestAttackEnum());
	}

	private IEnumerator TestAttackEnum()
	{
		while (true)
		{
			var rect = new Rect(RectCollision);
			rect.x += _pivotAttack.GetPosition().x + _orgine.x;
			rect.y += _pivotAttack.GetPosition().y + _orgine.y;
			var hits = EntityMapping.Instance.HitInRect(new RectOriented(rect, _pivotAttack.GetRotation(), _pivotAttack.GetPosition()));

			if (hits != null)
			{
				for (int i = 0; i < hits.Length; i++)
				{
					hits[i].ReceiveDamage(Damage);
				}
			}
			yield return null;
		}
	}

	/*
	public void OnCollisionEnter2D(Collision2D collision)
	{
		var script = collision.transform.GetComponent<EntityComponent>();
		if (script == null)
			return;
		_inflictDMG.InflictDmg(script);
	}
	*/
}

public interface IPivotAttack
{
	Quaternion GetRotation();
	Vector3 GetPosition();
}
