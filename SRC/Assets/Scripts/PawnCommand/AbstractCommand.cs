public abstract class BaseCommande
{
	protected ICommandUtils _iCommandUtils;
	protected float _timerCooldown;
	protected bool _executeCmd;

	public BaseCommande(ICommandUtils iCommandUtils)
	{
		_iCommandUtils = iCommandUtils;
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
			_iCommandUtils.SetGlobalCooldown();
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
	protected SpawnObjectData _spawnObjectData;

	protected CommandeSpawnObect(ICommandUtils iCommandUtils, SpawnObjectData spawnObjectData) : base(iCommandUtils)
	{
		_spawnObjectData = spawnObjectData;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		_iCommandUtils.SpawnObect(_spawnObjectData.Prefab, _iCommandUtils.GetPosition(), _iCommandUtils.GetRotation());
	}
}

public class CommandeAttackDistance : CommandeSpawnObect
{
	private readonly IEntity _iEntity;

	public CommandeAttackDistance(ICommandUtils iCommandUtils, SpawnObjectData spawnObjectData, IEntity iEntity) :
		base(iCommandUtils, spawnObjectData)
	{
		_iEntity = iEntity;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		var obj = _iCommandUtils.SpawnObect(_spawnObjectData.Prefab, _iCommandUtils.GetPosition(), _iCommandUtils.GetRotation());
		var script = obj.GetComponent<ProjectileComponent>();
		if (script != null)
		{
			script.Damage = _iEntity.GetAmountDamage();
		}
	}
}

public class CommandeAttackCac : CommandeSpawnObect
{
	private readonly IEntity _iEntity;

	public CommandeAttackCac(ICommandUtils iCommandUtils, SpawnObjectData spawnObjectData, IEntity iEntity) :
		base(iCommandUtils, spawnObjectData)
	{
		_iEntity = iEntity;
	}

	protected override void Execute()
	{
		_timerCooldown = _spawnObjectData.Cooldown;
		var obj = _iCommandUtils.SpawnObect(_spawnObjectData.Prefab, _iCommandUtils.GetPosition(), _iCommandUtils.GetRotation());
		var script = obj.GetComponent<ProjectileComponent>();
		if (script != null)
		{
			script.Damage = _iEntity.GetAmountDamage();
		}
	}
}