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

	public EntityComponent[] HitInRect(RectOriented rect)
	{
		var hitted = new List<IMapCollision>();

		var pos = new Vector2Int();
		var bl = rect.BLContainerPointOriented + SizeMap;
		var tr = rect.TRContainerPointOriented + SizeMap;


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

		var dataUniqueHit = hitted.Distinct().Where(item => RectOriented.Overlaps(rect, item.GetRectCollision()));

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

public struct RectOriented
{
	public readonly Vector2[] OrientedPoints;
	public readonly Vector2 BLContainerPointOriented;
	public readonly Vector2 TRContainerPointOriented;
	public readonly Rect OriginRect;
	public readonly Quaternion Rotation;
	public readonly Vector3 OriginePivot;

	public RectOriented(Rect rectOrigin, Quaternion rotation, Vector3 originPivot)
	{
		OriginRect = rectOrigin;
		Rotation = rotation;
		OriginePivot = originPivot;

		var rect = OriginRect;
		rect.x -= originPivot.x;
		rect.y -= originPivot.y;

		OrientedPoints = new Vector2[]
		{
			(rotation * new Vector2(rect.x, rect.y)) + originPivot,
			(rotation * new Vector2(rect.x + rect.width, rect.y)) + originPivot,
			(rotation * new Vector2(rect.x, rect.y + rect.height)) + originPivot,
			(rotation * new Vector2(rect.x + rect.width, rect.y + rect.height)) + originPivot,
		};
		Vector2 min = OrientedPoints[0];
		Vector2 max = OrientedPoints[0];

		for (int i = 1; i < 4; i++)
		{
			if (min.x > OrientedPoints[i].x)
				min.x = OrientedPoints[i].x;
			else if (max.x < OrientedPoints[i].x)
				max.x = OrientedPoints[i].x;

			if (min.y > OrientedPoints[i].y)
				min.y = OrientedPoints[i].y;
			else if (max.y < OrientedPoints[i].y)
				max.y = OrientedPoints[i].y;
		}

		BLContainerPointOriented = min;
		TRContainerPointOriented = max;
	}

	public override string ToString()
	{
		string str = "";
		str += "OriginePivot : " + OriginePivot.ToString() + "\n" +
			"OriginRect : " + OriginRect.ToString() + "\n" +
			"Rotation : " + Rotation.ToString() + "\n" +
			"BLContainerPointOriented : " + BLContainerPointOriented.ToString() + "\n" +
			"TRContainerPointOriented : " + TRContainerPointOriented.ToString() + "\n";

		for (int i = 0; i < 4; i++)
		{
			str += "OrientedPoints[" + i + "] : " + OrientedPoints[i].ToString() + "\n";
		}

		return str;
	}

	public static bool Overlaps (RectOriented rectOriented, Rect target)
	{
		for (int i = 0; i < 4; i++)
		{
			if (target.Contains(rectOriented.OrientedPoints[i], true))
				return true;
		}

		var points = GetPointsRect(target);
		var inverRotation = Quaternion.Inverse(rectOriented.Rotation);
		for (int i = 0; i < 4; i++)
		{
			points[i].x -= rectOriented.OriginePivot.x;
			points[i].y -= rectOriented.OriginePivot.y;
			Vector2 pointRect = (inverRotation * points[i]) + rectOriented.OriginePivot;
			if (rectOriented.OriginRect.Contains(pointRect))
				return true;
		}
		return false;
	}

	private static Vector2[] GetPointsRect(Rect rect)
	{
		return new Vector2[]
		{
			new Vector2(rect.x, rect.y),
			new Vector2(rect.x + rect.width, rect.y),
			new Vector2(rect.x, rect.y + rect.height),
			new Vector2(rect.x + rect.width, rect.y + rect.height),
		};
	}
}