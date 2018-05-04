using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemyPlugAI", menuName = "Config/EnemyPlugAI", order = 0)]
public class GeneratorEnemyPlugAI : AbstractGeneratorPlugAI
{
	public EnemyBasicPlugAI.EnemyBasicData Data;
	public override AbstractPlugAI Generate(PawnComponent pawn)
	{
		return new EnemyBasicPlugAI(pawn, Data);
	}
}

