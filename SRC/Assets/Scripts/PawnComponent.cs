using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnComponent : MonoBehaviour {


	public MoveBehaviour MoveData;
	public AttackBehaviour AttackData;

	private void Awake()
	{
		MoveData.Init(transform);
		AttackData.Init(transform);
	}

	private void FixedUpdate()
	{
		var delta = Time.deltaTime;
		MoveData.Tick(delta);
		AttackData.Tick(delta);
	}

	public void InputMove(Vector2 dir)
	{
		MoveData.SetDirection(dir);
	}

	public void InputAttack(bool launchAttack)
	{
		AttackData.SetInputAttack(launchAttack);
	}

	public void InputDirAttack(Vector2 dir)
	{
		AttackData.SetDirectionAttack(dir);
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
public class AttackBehaviour : AbstractPawnBehaviour, IInflictDmg
{
	public AttackComponent RefAttack;

	private bool _inputAttack;
	private EntityComponent _entity;

	public override void Init(Transform trans)
	{
		_entity = trans.GetComponent<EntityComponent>();
		if (RefAttack != null)
			RefAttack.Init(this);
	}

	public override void Tick(float deltaTime)
	{
		if (_inputAttack)
		{
			RefAttack.StartAttack();
		}
		_inputAttack = false;
	}

	public void SetInputAttack(bool inputAttack)
	{
		_inputAttack = inputAttack;
	}

	public void SetDirectionAttack(Vector2 dir)
	{
		RefAttack.SetDirAttack(dir);
	}

	public void InflictDmg(EntityComponent entity)
	{
		_entity.InflictDMG(entity);
	}
}

public interface IInflictDmg
{
	void InflictDmg(EntityComponent entity);
}
