using UnityEngine;

public abstract class AbstractGeneratorPlugAI : ScriptableObject
{
	public abstract AbstractPlugAI Generate(PawnComponent pawn);
}
