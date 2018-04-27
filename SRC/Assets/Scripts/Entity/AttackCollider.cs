using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

	private Collider2D _col;
	private IInflictDmg _inflictDMG;
	private Coroutine _routine;

	public void Init(IInflictDmg inflictDMG)
	{
		_col = GetComponent<Collider2D>();
		_inflictDMG = inflictDMG;
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
			var hits = EntityMapping.Instance.HitInRect(new Rect(_col.bounds.min, _col.bounds.max));

			if (hits != null)
			{
				for (int i = 0; i < hits.Length; i++)
				{
					_inflictDMG.InflictDmg(hits[i]);
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
