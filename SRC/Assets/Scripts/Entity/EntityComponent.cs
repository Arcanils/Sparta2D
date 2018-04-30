using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityComponent : MonoBehaviour, IMapCollision
{
	public float CurrentHP;
	public Entity Stats;
	public UIEntityStatus UIFeedback;

	private Collider2D _col;
	private float _baseHP;

	public void Awake()
	{
		_col = GetComponent<Collider2D>();
		Stats.CalculFinalStats();
		_baseHP = Stats.HP;
		CurrentHP = _baseHP;
	}

	public void Start()
	{
		EntityMapping.Instance.AddEntity(this);
	}

	public void ReceiveDMG(float dmgAmount)
	{
		CurrentHP -= Mathf.Max(dmgAmount - Stats.PhysicsRes, 0f);
		if (UIFeedback != null)
			UIFeedback.UpdateHPValue(CurrentHP / _baseHP);
		Debug.LogError("ReceiveDMG !");
		if (CurrentHP < 0f)
		{
			if (UIFeedback != null)
				UIFeedback.OnDeath();
			Destroy(gameObject);
		}
	}

	public void InflictDMG(EntityComponent target)
	{
		if (target == null && target != this)
			return;
		target.ReceiveDMG(Stats.PhysicsDMG);
		//target.ReceiveDMG(0f);
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
