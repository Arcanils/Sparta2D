using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig_", menuName = "Config/Entity", order = 0)]
public class EntityConfig : ScriptableObject
{
	public ControllerPawnTupple PrefabControllerPawn;
	public ControllerComponentConfig ControllerConfig;
	public PawnStructConfig PawnConfig;
}

[System.Serializable]
public struct PawnStructConfig
{
}

[System.Serializable]
public struct ControllerPawnTupple
{
	public PoolObjectComponent Controller;
	public PoolObjectComponent Pawn;
}