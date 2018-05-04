using UnityEngine;

public class PoolObjectComponent : MonoBehaviour
{
	public System.Action OnInitFromPool;
	public System.Action OnResetBeforeBackToPool;

	private PoolPrefab _refPool;

	public void SetPool(PoolPrefab RefPool)
	{
		_refPool = RefPool;
	}

	public void BackToPool()
	{

		if (_refPool == null)
		{
			if (OnResetBeforeBackToPool != null)
				OnResetBeforeBackToPool();
			Debug.LogError(name);
		}
		else
		{
			gameObject.SetActive(false);

			if (OnResetBeforeBackToPool != null)
				OnResetBeforeBackToPool();
			_refPool.BackToPool(this);
		}
	}

	public void InitObject()
	{
		if (OnInitFromPool != null)
			OnInitFromPool();
	}
}
