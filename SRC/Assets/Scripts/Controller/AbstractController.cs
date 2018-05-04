using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractController : MonoBehaviour
{
	protected PawnComponent _refPawn;
	protected PoolObjectComponent _poolObjectComponent;
	protected AbstractPlugAI _plugedAI;

	public abstract void Awake();

	public abstract void TickAI(float DeltaTime);

	public abstract void TickMove(float DeltaTime);
	public abstract void TickCmds(float DeltaTime);

	public abstract void Init(AbstractPlugAI Config);
	public abstract void ResetAfterDisable();
	public abstract void Destroy();

	public abstract void SetPawn(PawnComponent Pawn);
}
