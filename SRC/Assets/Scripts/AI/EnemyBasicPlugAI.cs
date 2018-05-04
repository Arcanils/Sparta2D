using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class EnemyBasicPlugAI : AbstractPlugAI
{
	[System.Serializable]
	public struct EnemyBasicData
	{
		public float RangeDetection;
		public float FireRateSearch;
		public bool GoTowardTarget;
		public bool Attack;

		public EnemyBasicData(float rangeDetection, float fireRateSearch, bool goTowardTarget, bool attack)
		{
			RangeDetection = rangeDetection;
			FireRateSearch = fireRateSearch;
			GoTowardTarget = goTowardTarget;
			Attack = attack;
		}
	}
	private Transform _trans;
	private LayerMask _layerPlayer;
	private EnemyBasicData _data;
	public EnemyBasicPlugAI(IControllPawn iInputPawn, EnemyBasicData data) : base(iInputPawn)
	{
		_data = data;
		_layerPlayer = LayerMask.GetMask("Player");
	}

	protected override IEnumerator AIEnum()
	{
		IPawnCollision target = null;

		while(true)
		{
			while (target == null)
			{
				target = GetTarget();

				if (target == null)
				{
					WaitDuration(_data.FireRateSearch);
					yield return null;
				}
			}
			
			while (target != null)
			{
				var posPawn = _iControllPawn.GetPosition();
				var posTarget = target.GetRectCollision().center;
				var dir = posTarget - posPawn;
				if (dir != Vector2.zero)
					dir.Normalize();

				if (_data.GoTowardTarget)
					_iControllPawn.InputMove(dir);

				if (_data.Attack)
				{
					_iControllPawn.InputDirAttack(dir);
					_iControllPawn.InputActionBind(0, true);
				}

				yield return null;
			}
		}
	}

	protected void WaitDuration(float duration)
	{
		var currentRoutine = _routineAI;
		_routineAI = WaitDurationEnum(duration, currentRoutine);
	}

	private IEnumerator WaitDurationEnum(float duration, IEnumerator routineToSet)
	{
		for (float t = 0f; t < duration; t += _deltaTime)
		{
			yield return null;
		}

		_routineAI = routineToSet;
	}

	private IPawnCollision GetTarget()
	{
		var pos = _iControllPawn.GetPosition();
		var hits = Physics2D.OverlapCircleAll(pos, _data.RangeDetection, _layerPlayer);

		var hit = hits.FirstOrDefault(item => item.GetComponent<IPawnCollision>() != null);

		return hit != null ? hit.GetComponent<IPawnCollision>() : null;
	}
}