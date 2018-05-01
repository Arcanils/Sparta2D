﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	private PawnComponent _pawn; 

	private static string keyHorizontal = "Horizontal";
	private static string keyVertical = "Vertical";
	private static string keyHorizontalAttack = "HorizontalAttack";
	private static string keyVeticalAttack = "VerticalAttack";
	private static string keyBind1 = "Fire1";
	private static string keyBind2 = "Fire2";

	private void Awake()
	{
		_pawn = FindObjectsOfType<PawnComponent>().First(item => item.tag == "Player");
	}

	private void Update()
	{
		var moveDir = new Vector2(Input.GetAxisRaw(keyHorizontal), Input.GetAxisRaw(keyVertical));
		if (moveDir != Vector2.zero)
			moveDir.Normalize();

		_pawn.InputMove(moveDir);

		var attackDir = new Vector2(Input.GetAxisRaw(keyHorizontalAttack), Input.GetAxisRaw(keyVeticalAttack));
		if (attackDir != Vector2.zero)
			attackDir.Normalize();

		_pawn.InputDirAttack(attackDir);

		if (Input.GetButtonDown(keyBind1))
			_pawn.CallInputBinded(0, true);
		if (Input.GetButtonUp(keyBind1))
			_pawn.CallInputBinded(0, false);

		if (Input.GetButtonDown(keyBind2))
			_pawn.CallInputBinded(1, true);
		if (Input.GetButtonUp(keyBind2))
			_pawn.CallInputBinded(1, false);
	}
}
