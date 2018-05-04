using System.Collections.Generic;
using UnityEngine;

public class PoolPrefab
{
	private Stack<PoolObjectComponent> _poolAvailable;
	private List<PoolObjectComponent> _pool;

	private readonly GameObject _prefab;
	private readonly Transform _container;

	public PoolPrefab()
	{
		_prefab = null;
	}

	public PoolPrefab(PoolObjectComponent Prefab, Transform Container, int Size)
	{
		_prefab = Prefab.gameObject;
		_container = Container;
		FillPool(Size);
	}

	public PoolObjectComponent GetItem()
	{
		var item = _poolAvailable.Count != 0 ? _poolAvailable.Pop() : CreateNewItem();
		/*
		item.transform.position = PositionOrigine;
		item.gameObject.SetActive(true);
		item.InitObject();
		*/
		return item;
	}

	public void BackToPool(PoolObjectComponent Object)
	{
		_poolAvailable.Push(Object);
		Object.gameObject.SetActive(false);
	}

	private PoolObjectComponent CreateNewItem()
	{
		var newInstance = GameObject.Instantiate<GameObject>(_prefab, _container, true);
		var script = newInstance.GetComponent<PoolObjectComponent>();
		_pool.Add(script);
		script.SetPool(this);
		return script;
	}

	private void FillPool(int PoolInitSize)
	{
		_poolAvailable = new Stack<PoolObjectComponent>(PoolInitSize);
		_pool = new List<PoolObjectComponent>(PoolInitSize);

		for (int i = 0; i < PoolInitSize; ++i)
		{
			var newInstance = GameObject.Instantiate<GameObject>(_prefab, _container, true);
			var script = newInstance.GetComponent<PoolObjectComponent>();
			_pool.Add(script);
			_poolAvailable.Push(script);
			script.SetPool(this);
			newInstance.SetActive(false);
		}
	}
}

