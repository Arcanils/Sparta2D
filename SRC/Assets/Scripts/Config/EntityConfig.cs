using System;
using UnityEngine;

[CreateAssetMenu(fileName = "EntityConfig_", menuName = "Config/Entity", order = 0)]
public class EntityConfig : ScriptableObject
{
	public ControllerPawnTupple PrefabControllerPawn;
	public AbstractGeneratorPlugAI ControllerConfig;
	public PawnStructConfig PawnConfig;
}

[System.Serializable]
public struct PawnStructConfig
{
	public EntityDataConfig EntityData;
	public EntityEquipement EquipementsData;
	public BaseCommandeScriptable[] CmdsScriptable;

	public BaseCommande[] GetPawnCmds(PawnComponent refPawn, EntityComponent entity)
	{
		var cmds = new BaseCommande[CmdsScriptable.Length];
		for (int i = 0; i < cmds.Length; i++)
		{
			cmds[i] = CmdsScriptable[i].Generate(refPawn, entity);
		}

		return cmds;
	}

	public EntityComponent GetEntity()
	{
		return new EntityComponent(new Entity(EntityData, EquipementsData));
	}
}

public abstract class BaseCommandeScriptable : ScriptableObject
{
	public abstract BaseCommande Generate(PawnComponent pawn, EntityComponent entity);
}

[System.Serializable]
public struct ControllerPawnTupple
{
	public PoolObjectComponent Controller;
	public PoolObjectComponent Pawn;
}