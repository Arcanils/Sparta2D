using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class AbstractPlugAI
{
	protected float _deltaTime;
	protected IEnumerator _routineAI;
	protected IControllPawn _iControllPawn;

	public AbstractPlugAI(IControllPawn iInputPawn)
	{
		_iControllPawn = iInputPawn;
		_routineAI = AIEnum();
	}

	public void Tick(float deltaTime)
	{
		_deltaTime = deltaTime;
		_routineAI.MoveNext();
	}

	protected abstract IEnumerator AIEnum();
}

public struct CmdInput
{
	public readonly int Key;
	public readonly bool Value;

	public CmdInput(int key, bool value)
	{
		Key = key;
		Value = value;
	}
}