using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityComponent : MonoBehaviour
{
	public float CurrentHP;
	public Entity Stats;

	public void ReceiveDMG(float dmgAmount)
	{
		CurrentHP -= Mathf.Max(dmgAmount - Stats.PhysicsRes, 0f);
		Debug.LogError("ReceiveDMG !");
	}

	public void InflictDMG(EntityComponent target)
	{
		if (target == null)
			return;
		target.ReceiveDMG(Stats.PhysicsDMG);
		Debug.LogError("InflictDMG !");
	}
}
