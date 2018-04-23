using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerController : MonoBehaviour {


	private PawnComponent _pawn;

	private static string keyHorizontal = "Horizontal";
	private static string keyVertical = "Vertical";

	private void Awake()
	{
		_pawn = FindObjectsOfType<PawnComponent>().First(item => item.tag == "Player");
	}

	private void Update()
	{
		var moveDir = new Vector2(Input.GetAxisRaw(keyHorizontal), Input.GetAxisRaw(keyVertical));
		if (moveDir != Vector2.zero)
			moveDir.Normalize();

		_pawn.Move(moveDir);
	}
}
