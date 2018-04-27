using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityComponent : MonoBehaviour, IMapCollision
{
	public float CurrentHP;
	public Entity Stats;

	private Collider2D _col;

	public void Awake()
	{
		_col = GetComponent<Collider2D>();
	}

	public void Start()
	{
		EntityMapping.Instance.AddEntity(this);
	}

	public void ReceiveDMG(float dmgAmount)
	{
		//CurrentHP -= Mathf.Max(dmgAmount - Stats.PhysicsRes, 0f);
		Debug.LogError("ReceiveDMG !");
	}

	public void InflictDMG(EntityComponent target)
	{
		if (target == null && target != this)
			return;
		//target.ReceiveDMG(Stats.PhysicsDMG);
		target.ReceiveDMG(0f);
		Debug.LogError("InflictDMG !");
	}

	public Rect GetRectCollision()
	{
		return new Rect(_col.bounds.min, _col.bounds.size);
	}

	public EntityComponent GetEntity()
	{
		return this;
	}
}
