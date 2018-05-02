using System.Collections.Generic;
using UnityEngine;

public class PoolManager
{
	private Dictionary<PoolObjectComponent, PoolPrefab> _mapPools;

	private readonly Transform _container;

	public PoolManager() { }

	public PoolManager(Transform Container)
	{
		_container = Container;
		_mapPools = new Dictionary<PoolObjectComponent, PoolPrefab>();
	}

	public void CreatePool(PoolObjectComponent PrefabToPool, int Size)
	{
		if (_mapPools.ContainsKey(PrefabToPool))
		{
			Debug.LogWarning("[" + PrefabToPool.gameObject.name + "] is already in it");
			return;
		}

		GameObject goContainer = new GameObject("[" + PrefabToPool.name + "]");
		Transform transContainer = goContainer.transform;
		transContainer.parent = _container;
		_mapPools[PrefabToPool] = new PoolPrefab(PrefabToPool, transContainer, Size);
	}

	public PoolObjectComponent GetItem(PoolObjectComponent PrefabRef)
	{
		if (PrefabRef == null)
		{
			Debug.LogError("Get null ref");
			return null;
		}
		else if (_mapPools.ContainsKey(PrefabRef) == false)
		{
			Debug.LogWarning("[" + PrefabRef + "] not present in pool");
			CreatePool(PrefabRef, 10);
		}
		return _mapPools[PrefabRef].GetItem();
	}

	public T GetItem<T>(PoolObjectComponent PrefabRef)
	{
		return GetItem(PrefabRef).GetComponent<T>();
	}
}