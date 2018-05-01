using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IABasicController : MonoBehaviour {

	public Transform Target;
	public PawnComponent Pawn;

	public bool GoTowardTarget;
	public bool Attack;

	private Transform _trans;

	private void Awake()
	{
		_trans = Pawn.transform;
	}
	void Update ()
	{
		var dir = Target.position - _trans.position;
		dir.Normalize();

		if (GoTowardTarget)
			Pawn.InputMove(dir);

		if (Attack)
		{
			Pawn.InputDirAttack(dir);
			Pawn.CallInputBinded(0, true);
		}
	}
}
