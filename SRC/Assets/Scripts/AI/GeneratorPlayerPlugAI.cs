using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "PlayerPlugAI", menuName = "Config/PlayerPlugAI", order = 0)]
public class GeneratorPlayerPlugAI : AbstractGeneratorPlugAI
{
	public override AbstractPlugAI Generate(PawnComponent pawn)
	{
		return new PlayerPlugAI(pawn);
	}
}

