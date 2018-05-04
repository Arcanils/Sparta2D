using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class LvlConfig : ScriptableObject
{
	public SpawnEntityData[] Enemies;
}

public struct SpawnEntityData
{
	public enum ESpawnPosition
	{
		RANDOM,
		LEFT,
		TOP,
		BOT,
		RIGHT,
	}

	public ESpawnPosition Position;
	public Vector2 Offset;
	public EntityConfig EnemyConfig;
}
