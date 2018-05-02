using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityComponent: IEntity
{
	private UIEntityStatus _uiEntityStatus;
	private float _currentHP;
	private Entity _entityStats;
	
	private float _baseHP;

	private IDeath _iDeath;

	public EntityComponent(Entity entity)
	{
		this._entityStats = entity;
	}

	public void Init(IDeath iDeath)
	{
		_iDeath = iDeath;
		_entityStats.CalculFinalStats();
		_baseHP = _entityStats.HP;
		_currentHP = _baseHP;
	}
	

	public void InflictDMG(IEntity target)
	{
		if (target == null && target != this)
			return;
		target.ReceiveDamage(_entityStats.PhysicsDMG);
		//target.ReceiveDMG(0f);
		Debug.LogError("InflictDMG !");
	}
	

	public void ReceiveDamage(float damage)
	{
		_currentHP -= Mathf.Max(damage - _entityStats.PhysicsRes, 0f);
		if (_uiEntityStatus != null)
			_uiEntityStatus.UpdateHPValue(_currentHP / _baseHP);
		Debug.LogError("ReceiveDMG !");
		if (_currentHP < 0f)
		{
			if (_uiEntityStatus != null)
				_uiEntityStatus.OnDeath();
			_iDeath.OnDeath();
		}
	}

	public float GetAmountDamage()
	{
		return _entityStats.PhysicsDMG;
	}
}
