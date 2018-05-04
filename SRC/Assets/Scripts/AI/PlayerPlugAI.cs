using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPlugAI : AbstractPlugAI
{
	private static string keyHorizontal = "Horizontal";
	private static string keyVertical = "Vertical";
	private static string keyHorizontalAttack = "HorizontalAttack";
	private static string keyVeticalAttack = "VerticalAttack";
	private static string keyBind1 = "Fire1";
	private static string keyBind2 = "Fire2";

	public PlayerPlugAI(IControllPawn iInputPawn) : base(iInputPawn)
	{

	}

	protected override IEnumerator AIEnum()
	{
		while (true)
		{
			if (_iControllPawn == null)
				break;
			var dir = new Vector2(Input.GetAxisRaw(keyHorizontal), Input.GetAxisRaw(keyVertical));
			if (dir != Vector2.zero)
				dir.Normalize();
			_iControllPawn.InputMove(dir);

			var dirAttack = new Vector2(Input.GetAxisRaw(keyHorizontalAttack), Input.GetAxisRaw(keyVeticalAttack));
			if (dirAttack != Vector2.zero)
				dirAttack.Normalize();

			_iControllPawn.InputDirAttack(dirAttack);

			if (Input.GetButtonDown(keyBind1))
				_iControllPawn.InputActionBind(0, true);
			if (Input.GetButtonUp(keyBind1))
				_iControllPawn.InputActionBind(0, false);

			if (Input.GetButtonDown(keyBind2))
				_iControllPawn.InputActionBind(1, true);
			if (Input.GetButtonUp(keyBind2))
				_iControllPawn.InputActionBind(1, false);

			yield return null;
		}
	}
}
