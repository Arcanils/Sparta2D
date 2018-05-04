using UnityEngine;

public class PoolInitializer : MonoBehaviour
{
	[System.Serializable]
	public struct DataPool
	{
		public PoolObjectComponent Prefab;
		public int SizePreload;

		public DataPool(PoolObjectComponent Prefab, int SizePreload)
		{
			this.Prefab = Prefab;
			this.SizePreload = SizePreload;
		}
	}

	public DataPool[] DataPoolPrefab;

	public void Start()
	{
		for (int i = 0, iLength = DataPoolPrefab.Length; i < iLength; ++i)
		{
			Main.Instance.PoolManagerInstance.CreatePool(DataPoolPrefab[i].Prefab, DataPoolPrefab[i].SizePreload);
		}
	}
}