using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCollider : MonoBehaviour {

	private Collider2D _col;
	private IInflictDmg _inflictDMG;

	public void Init(IInflictDmg inflictDMG)
	{
		_col = GetComponent<Collider2D>();
		_inflictDMG = inflictDMG;
	}

	public void SetActiveAttack(bool enable)
	{
		_col.enabled = enable;
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		var script = collision.transform.GetComponent<EntityComponent>();
		if (script == null)
			return;
		_inflictDMG.InflictDmg(script);
	}
}
