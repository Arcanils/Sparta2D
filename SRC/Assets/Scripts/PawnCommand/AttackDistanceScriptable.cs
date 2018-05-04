using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PawnCommands
{
	[CreateAssetMenu(fileName = "AttackDistance", menuName = "Commands/AttackDistance")]
	public class AttackDistanceScriptable : BaseCommandeScriptable
	{
		public SpawnObjectData Data;

		public override BaseCommande Generate(PawnComponent pawn, EntityComponent entity)
		{
			return new CommandeAttackDistance(pawn, Data, entity);
		}
	}
}