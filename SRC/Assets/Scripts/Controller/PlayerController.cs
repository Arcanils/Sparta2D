using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : BaseController
{
	private static string keyHorizontal = "Horizontal";
	private static string keyVertical = "Vertical";
	private static string keyHorizontalAttack = "HorizontalAttack";
	private static string keyVeticalAttack = "VerticalAttack";
	private static string keyBind1 = "Fire1";
	private static string keyBind2 = "Fire2";

	public override void Init(ControllerComponentConfig Config)
	{
		base.Init(Config);
	}

	public override void TickAI(float DeltaTime)
	{
		var moveDir = new Vector2(Input.GetAxisRaw(keyHorizontal), Input.GetAxisRaw(keyVertical));
		if (moveDir != Vector2.zero)
			moveDir.Normalize();

		_refPawn.InputMove(moveDir);

		var attackDir = new Vector2(Input.GetAxisRaw(keyHorizontalAttack), Input.GetAxisRaw(keyVeticalAttack));
		if (attackDir != Vector2.zero)
			attackDir.Normalize();

		_refPawn.InputDirAttack(attackDir);

		if (Input.GetButtonDown(keyBind1))
			_refPawn.CallInputBinded(0, true);
		if (Input.GetButtonUp(keyBind1))
			_refPawn.CallInputBinded(0, false);

		if (Input.GetButtonDown(keyBind2))
			_refPawn.CallInputBinded(1, true);
		if (Input.GetButtonUp(keyBind2))
			_refPawn.CallInputBinded(1, false);
	}
}
