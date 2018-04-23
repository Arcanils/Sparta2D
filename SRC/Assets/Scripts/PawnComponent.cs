using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawnComponent : MonoBehaviour {


	private Transform _trans;
	private Rigidbody2D _rigid;
	private Vector2 _position;

	private void Awake()
	{
		_trans = transform;
		_rigid = GetComponent<Rigidbody2D>();
	}

	private void FixedUpdate()
	{
		_position = new Vector2(_trans.position.x, _trans.position.y) + _moveDir * SpeedMove * Time.fixedDeltaTime;
		_rigid.MovePosition(_position);
	}

	public float SpeedMove = 10f;

	private Vector2 _moveDir;

	public void Move(Vector2 dir)
	{
		_moveDir = dir;
	}
}
