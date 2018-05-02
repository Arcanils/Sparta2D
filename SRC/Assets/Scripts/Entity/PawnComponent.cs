using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnComponent : MonoBehaviour, ICommandUtils, IDeath, IPawnCollision
{
	public MoveBehaviour MoveData;

	private EntityComponent _entity;
	private BaseCommande[] _cmds;

	private Transform _trans;
	private Quaternion _rotationAttack;
	private Collider2D _col;

	public void Init(PawnStructConfig config)
	{
		_trans = transform;
		_col = GetComponent<Collider2D>();
		MoveData.Init(transform);

		_entity = config.GetEntity();
		_entity.Init(this);
		_cmds = config.GetPawnCmds(this, _entity);

		EntityMapping.Instance.AddEntity(this);
	}

	public void TickMove(float deltaTime)
	{
		MoveData.Tick(deltaTime);
	}

	public void TickCmds(float deltaTime)
	{
		for (int i = 0, iLength = _cmds.Length; i < iLength; i++)
		{
			_cmds[i].Tick(deltaTime);
		}
	}

	public void InputMove(Vector2 dir)
	{
		MoveData.SetDirection(dir);
	}

	public void InputDirAttack(Vector2 dir)
	{
		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		_rotationAttack = Quaternion.Euler(0f, 0f, angle);
	}

	public void CallInputBinded(int indexInput, bool input)
	{
		_cmds[indexInput].InputExecute(input);
	}

	Quaternion ICommandUtils.GetRotation()
	{
		return _rotationAttack;
	}

	Vector3 ICommandUtils.GetPosition()
	{
		return _trans.position;
	}

	GameObject ICommandUtils.SpawnObect(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		return GameObject.Instantiate<GameObject>(prefab, pos, rot);
	}

	void ICommandUtils.SetGlobalCooldown(float duration)
	{
		for (int i = 0; i < _cmds.Length; i++)
		{
			_cmds[i].SetGlobalCooldown(duration);
		}
	}

	void IDeath.OnDeath()
	{
		EntityMapping.Instance.RemoveEntity(this);
		Destroy(gameObject);
	}

	Rect IPawnCollision.GetRectCollision()
	{
		return new Rect(_col.bounds.min, _col.bounds.size);
	}

	public IEntity GetEntity()
	{
		return _entity;
	}
}

public abstract class AbstractPawnBehaviour
{
	public abstract void Init(Transform trans);
	public abstract void Tick(float deltaTime);
}

[System.Serializable]
public class MoveBehaviour : AbstractPawnBehaviour
{
	public float SpeedMove = 10f;

	private Transform _trans;
	private Rigidbody2D _rigid;
	private Vector2 _position;

	private Vector2 _moveDir;
	

	public override void Init(Transform trans)
	{
		_trans = trans;
		_rigid = trans.GetComponent<Rigidbody2D>();
	}

	public override void Tick(float deltaTime)
	{
		_position = new Vector2(_trans.position.x, _trans.position.y) + _moveDir * SpeedMove * Time.fixedDeltaTime;
		_rigid.MovePosition(_position);
	}


	public void SetDirection(Vector2 dir)
	{
		_moveDir = dir;
	}
}

[System.Serializable]
public class AttackBehaviour : AbstractPawnBehaviour
{
	public AttackComponent RefAttackCac;

	private bool _inputAttackCac;
	private bool _inputAttackDist;
	private EntityComponent _entity;

	public override void Init(Transform trans)
	{
		_entity = trans.GetComponent<EntityComponent>();
		if (RefAttackCac != null)
			RefAttackCac.Init();
	}

	public override void Tick(float deltaTime)
	{
		if (_inputAttackCac)
		{
			RefAttackCac.StartAttack();
		}
		_inputAttackCac = false;
	}

	public void SetInputAttack(bool inputAttack, bool isDistance)
	{
		_inputAttackCac = inputAttack;
	}

	public void SetDirectionAttack(Vector2 dir)
	{
		RefAttackCac.SetDirAttack(dir);
	}

	public void InflictDmg(EntityComponent entity)
	{
		_entity.InflictDMG(entity);
	}
}


public interface IEntity
{
	void ReceiveDamage(float damage);
	float GetAmountDamage();
}


public interface ICommandUtils
{
	Quaternion GetRotation();
	Vector3 GetPosition();
	GameObject SpawnObect(GameObject prefab, Vector3 pos, Quaternion rot);
	void SetGlobalCooldown(float duration = 0.5f);
}

[System.Serializable]
public class SpawnObjectData
{
	public GameObject Prefab;
	public float Cooldown;
}



public interface IDeath
{
	void OnDeath();
}