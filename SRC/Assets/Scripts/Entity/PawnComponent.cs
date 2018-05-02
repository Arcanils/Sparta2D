using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnComponent : MonoBehaviour, IGlobalCooldown, IPawn
{
	public SpawnObjectData[] SpawnData;

	public MoveBehaviour MoveData;
	public AttackBehaviour AttackData;

	public BaseCommande[] _cmds;

	private Transform _trans;
	private Quaternion _rotationAttack;

	private void Awake()
	{
		_trans = transform;
		MoveData.Init(transform);
		AttackData.Init(transform);
		_cmds = new BaseCommande[SpawnData.Length];
		var iEntity = GetComponent<IEntity>();
		for (int i = 0; i < _cmds.Length; i++)
		{
			_cmds[i] = new CommandeAttackDistance(this, this, iEntity, SpawnData[i]);
		}
	}

	private void FixedUpdate()
	{
		var delta = Time.deltaTime;
		MoveData.Tick(delta);

		for (int i = 0; i < _cmds.Length; i++)
		{
			_cmds[i].Tick(delta);
		}
	}

	public void Init(PawnStructConfig config)
	{

	}

	public void InputMove(Vector2 dir)
	{
		MoveData.SetDirection(dir);
	}

	public void InputDirAttack(Vector2 dir)
	{
		//AttackData.SetDirectionAttack(dir);

		float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

		_rotationAttack = Quaternion.Euler(0f, 0f, angle);
	}

	public void CallInputBinded(int indexInput, bool input)
	{
		_cmds[indexInput].InputExecute(input);
	}

	Quaternion IPawn.GetRotation()
	{
		return _rotationAttack;
	}

	Vector3 IPawn.GetPosition()
	{
		return _trans.position;
	}

	GameObject IPawn.SpawnObect(GameObject prefab, Vector3 pos, Quaternion rot)
	{
		return GameObject.Instantiate<GameObject>(prefab, pos, rot);
	}

	void IGlobalCooldown.SetGlobalCooldown(float duration)
	{
		for (int i = 0; i < _cmds.Length; i++)
		{
			_cmds[i].SetGlobalCooldown(duration);
		}
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
	public AttackComponent RefAttackCac;

	private bool _inputAttackCac;
	private bool _inputAttackDist;
	private EntityComponent _entity;

	public override void Init(Transform trans)
	{
		_entity = trans.GetComponent<EntityComponent>();
		if (RefAttackCac != null)
			RefAttackCac.Init(this);
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

public abstract class BaseCommande
{
	protected IGlobalCooldown _iGlobalCooldown;
	protected float _timerCooldown;
	protected bool _executeCmd;

	public BaseCommande(IGlobalCooldown iGlobalCooldown)
	{
		_iGlobalCooldown = iGlobalCooldown;
	}

	public void InputExecute(bool input)
	{
		_executeCmd = input;
	}
	public void Tick(float deltaTime)
	{
		_timerCooldown -= deltaTime;
		if (_executeCmd && _timerCooldown < 0f)
		{
			_timerCooldown = 2f;
			Execute();
			_iGlobalCooldown.SetGlobalCooldown();
		}
		_executeCmd = false;
	}

	public void SetGlobalCooldown(float duration)
	{
		if (_timerCooldown < duration)
		{
			_timerCooldown = duration;
		}
	}

	protected abstract void Execute();
}

public abstract class CommandeSpawnObect : BaseCommande
{
	protected IPawn _iPawn;
	protected SpawnObjectData _spawnObjectData;

	public CommandeSpawnObect(IGlobalCooldown iGlobalCooldown, IPawn iPawn ,SpawnObjectData spawnObjectData) : base(iGlobalCooldown)
	{
		_spawnObjectData = spawnObjectData;
		_iPawn = iPawn;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		_iPawn.SpawnObect(_spawnObjectData.Prefab, _iPawn.GetPosition(), _iPawn.GetRotation());
	}
}

public class CommandeAttackDistance : CommandeSpawnObect
{
	private IEntity _iEntity;

	public CommandeAttackDistance(IGlobalCooldown iGlobalCooldown, IPawn iPawn, IEntity iEntity,
		SpawnObjectData spawnObjectData) :
		base(iGlobalCooldown, iPawn, spawnObjectData)
	{
		_iEntity = iEntity;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		var obj = _iPawn.SpawnObect(_spawnObjectData.Prefab, _iPawn.GetPosition(), _iPawn.GetRotation());
		var script = obj.GetComponent<ProjectileComponent>();
		if (script != null)
		{
			script.Damage = _iEntity.GetAmountDamage();
		}
	}
}

public class CommandeAttackCac: CommandeSpawnObect
{
	private IEntity _iEntity;

	public CommandeAttackCac(IGlobalCooldown iGlobalCooldown, IPawn iPawn, IEntity iEntity,
		SpawnObjectData spawnObjectData) :
		base(iGlobalCooldown, iPawn, spawnObjectData)
	{
		_iEntity = iEntity;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		var obj = _iPawn.SpawnObect(_spawnObjectData.Prefab, _iPawn.GetPosition(), _iPawn.GetRotation());
		var script = obj.GetComponent<ProjectileComponent>();
		if (script != null)
		{
			script.Damage = _iEntity.GetAmountDamage();
		}
	}
}

public interface IEntity
{
	void ReceiveDamage(float damage);
	float GetAmountDamage();
}

public interface IGlobalCooldown
{
	void SetGlobalCooldown(float duration = 0.5f);
}

public interface IPawn
{
	Quaternion GetRotation();
	Vector3 GetPosition();
	GameObject SpawnObect(GameObject prefab, Vector3 pos, Quaternion rot);
}

[System.Serializable]
public class SpawnObjectData
{
	public GameObject Prefab;
	public float Cooldown;
}

public interface IInflictDmg
{
	void InflictDmg(EntityComponent entity);
}
