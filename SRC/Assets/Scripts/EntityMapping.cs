using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityMapping : MonoBehaviour {

	public Vector2 SizeMap;
	public int NCollumns;
	public int NLines;

	private Dictionary<Vector2Int, List<IMapCollision>> _map;
	private List<IMapCollision> _entitiesToTrack;

	private void Awake()
	{
		_map = new Dictionary<Vector2Int, List<IMapCollision>>();
		_entitiesToTrack = new List<IMapCollision>();
	}

	public void AddEntity(IMapCollision entity)
	{
		_entitiesToTrack.Add(entity);
	}

	private void UpdateMap()
	{
		_map.Clear();
	}
}

public interface IMapCollision
{
	Rect GetRect();
}
