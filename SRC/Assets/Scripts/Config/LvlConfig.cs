using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LvlConfig_", menuName = "Config/Lvl")]
public class LvlConfig : ScriptableObject
{
	public SpawnEntityData[] Enemies;
}

[System.Serializable]
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
