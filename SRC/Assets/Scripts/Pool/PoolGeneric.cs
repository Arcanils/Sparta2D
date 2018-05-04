using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolGeneric<TObject> where TObject : Object, new()
{
	public int PoolSize = 20;

	private List<TObject> _pool;
	private List<bool> _inUse;

	public PoolGeneric()
	{
		FillPool();
	}


	public TObject GetItem()
	{
		for (int i = 0; i < PoolSize; ++i)
		{
			if (!_inUse[i])
			{
				_inUse[i] = true;
				return _pool[i];
			}
		}

		return CreateNewItem();
	}

	public bool ReturnItem(TObject Item)
	{
		for (int i = 0; i < PoolSize; ++i)
		{
			if (Item == _pool[i])
			{
				_inUse[i] = false;
				return true;
			}
		}

		return false;
	}

	private TObject CreateNewItem()
	{
		_pool.Add(new TObject());
		_inUse.Add(false);
		return _pool[PoolSize++];
	}

	private void FillPool()
	{
		_pool = new List<TObject>(PoolSize);
		_inUse = new List<bool>(PoolSize);

		for (int i = 0; i < PoolSize; ++i)
		{
			_pool[i] = new TObject();
		}
	}

};

