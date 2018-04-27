using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EntityMapping : MonoBehaviour {

	public static EntityMapping Instance;

	public Vector2 SizeMap;
	public int NCollumns;
	public int NLines;

	private Dictionary<Vector2Int, List<IMapCollision>> _map;
	private List<IMapCollision> _entitiesToTrack;

	private void Awake()
	{
		Instance = this;
		_map = new Dictionary<Vector2Int, List<IMapCollision>>();
		_entitiesToTrack = new List<IMapCollision>();
	}

	private void LateUpdate()
	{
		UpdateMap();
	}

	public void AddEntity(IMapCollision entity)
	{
		_entitiesToTrack.Add(entity);
	}

	public EntityComponent[] HitInRect(Rect rect)
	{
		var hitted = new List<IMapCollision>();

		var pos = new Vector2Int();
		var bl = rect.min + SizeMap;
		var tr = rect.max + SizeMap;


		var begX = (int)(bl.x / NCollumns);
		var endX = (int)(tr.x / NCollumns);
		var begY = (int)(bl.y / NLines);
		var endY = (int)(tr.y / NLines);

		for (int j = begX; j <= endX; j++)
		{
			for (int z = begY; z <= endY; z++)
			{
				pos.Set(z, j);

				List<IMapCollision> entities;
				if (!_map.TryGetValue(pos, out entities))
					continue;
				for (int i = entities.Count - 1; i >= 0; --i)
				{
					if (hitted.Contains(entities[i]))
						continue;

					hitted.Add(entities[i]);
				}
			}
		}

		if (hitted.Count == 0)
			return null;

		var dataUniqueHit = hitted.Distinct().Where(item => rect.Overlaps(item.GetRectCollision(), true));

		if (dataUniqueHit == null)
			return null;
		
		return dataUniqueHit.Select(item => item.GetEntity()).ToArray();
	}

	private void UpdateMap()
	{
		_map.Clear();
		var pos = new Vector2Int();
		for (int i = _entitiesToTrack.Count - 1; i >= 0; --i)
		{
			var rect = _entitiesToTrack[i].GetRectCollision();
			var bl = rect.min + SizeMap;
			var tr = rect.max + SizeMap;

			var begX = (int)(bl.x / NCollumns);
			var endX = (int)(tr.x / NCollumns);
			var begY = (int)(bl.y / NLines);
			var endY = (int)(tr.y / NLines);

			for (int j = begX; j <= endX; j++)
			{
				for (int z = begY; z <= endY; z++)
				{
					pos.Set(z, j);
					if (!_map.ContainsKey(pos))
						_map[pos] = new List<IMapCollision>();
					_map[pos].Add(_entitiesToTrack[i]);
				}
			}
		}
	}
}

public interface IMapCollision
{
	Rect GetRectCollision();
	EntityComponent GetEntity();
}
