using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseController : AbstractController
{
	public override void SetPawn(PawnComponent Pawn)
	{
		if (Pawn)
		{
			_refPawn = Pawn;
		}
	}

	public override void Awake()
	{
		_poolObjectComponent = GetComponent<PoolObjectComponent>();

		if (_poolObjectComponent != null)
		{
			_poolObjectComponent.OnResetBeforeBackToPool += ResetAfterDisable;
		}
	}

	public override void Init(AbstractPlugAI Config)
	{
		_plugedAI = Config;
		Main.Instance.GameplayLoopInstance.SubElement(this);
	}

	public override void ResetAfterDisable()
	{
		Main.Instance.GameplayLoopInstance.RemoveElement(this);

		if (_refPawn)
		{
			_refPawn = null;
		}

		_plugedAI = null;
	}

	public override void Destroy()
	{
		if (_poolObjectComponent != null)
			_poolObjectComponent.BackToPool();
		else
			Destroy(gameObject);
	}

	public override void TickAI(float deltaTime)
	{
		_plugedAI.Tick(deltaTime);
	}

	public override void TickMove(float deltaTime)
	{
		_refPawn.TickMove(deltaTime);
	}

	public override void TickCmds(float deltaTime)
	{
		_refPawn.TickCmds(deltaTime);
	}
}

public interface IControllPawn
{
	void InputMove(Vector2 dir);
	void InputDirAttack(Vector2 dir);
	void InputActionBind(int key, bool value);
	Vector2 GetPosition();
}
