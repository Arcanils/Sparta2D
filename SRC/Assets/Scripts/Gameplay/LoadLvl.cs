using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LvlLoader
{
	public Transform[] SpawnPoints;
	public LvlConfig[] LvlsData;

	private EntityFactory _entityFactory;

	public void Init(EntityFactory Factory)
	{
		_entityFactory = Factory;
	}

	public List<AbstractController> LoadLvl(int indexLevel)
	{
		var entitiesSpawned = new List<AbstractController>();
		if (indexLevel >= LvlsData.Length)
			return entitiesSpawned;

		var data = LvlsData[indexLevel];
		var entities = data.Enemies;

		for (int i = 0; i < entities.Length; i++)
		{
			var entity = entities[i];
			var transPosition = entity.Position == SpawnEntityData.ESpawnPosition.RANDOM ?
				SpawnPoints[UnityEngine.Random.Range(0, SpawnPoints.Length) + 1] : SpawnPoints[((int)entity.Position) - 1];
			PawnComponent pawn;
			AbstractController controller;
			_entityFactory.GetNewEntity(entity.EnemyConfig, transPosition.position, out pawn, out controller);

			entitiesSpawned.Add(controller);
		}

		return entitiesSpawned;
	}
	/*
public void CreateLvl(LvlConfig Config)
{
	_entityFactory.GetNewEntity(Config.Player, new Vector3(-6f, 0f, 0f));
}*/

}

