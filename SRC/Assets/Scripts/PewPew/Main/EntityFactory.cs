using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFactory
{
	private PoolManager _pool;


	public EntityFactory(PoolManager Pool)
	{
		Init(Pool);
	}


	public void Init(PoolManager Pool)
	{
		_pool = Pool;
	}
	public PawnComponent GetNewPawn(EntityConfig Data, Vector3 Position)
	{
		var pawnComponent = _pool.GetItem<PawnComponent>(Data.PrefabControllerPawn.Pawn);
		pawnComponent.transform.position = Position;
		pawnComponent.Init(Data.PawnConfig);
		pawnComponent.gameObject.SetActive(true);
		return pawnComponent;
	}

	public AbstractController GetNewController(EntityConfig Data, PawnComponent InstancePawn)
	{
		var controller = _pool.GetItem<AbstractController>(Data.PrefabControllerPawn.Controller);
		controller.Init(Data.ControllerConfig);
		controller.SetPawn(InstancePawn);
		controller.gameObject.SetActive(true);
		return controller;
	}

	public GameObject GetNewEntity(EntityConfig Data, Vector3 Position)
	{
		var Pawn = GetNewPawn(Data, Position);
		var Controller = GetNewController(Data, Pawn);

		return Controller.gameObject;
	}
}
