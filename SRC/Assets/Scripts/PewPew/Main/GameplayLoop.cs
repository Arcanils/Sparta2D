using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayLoop
{
	private List<BaseController> _listController;
	private int _listLength;
	private int _capacity;
	private GameCamera _cam;

	public GameplayLoop(GameCamera CameraLogic, int SizeListController = 100)
	{
		_cam = CameraLogic;

		_listController = new List<BaseController>(SizeListController);
		_listLength = 0;
		_capacity = SizeListController;
	}

	public void TickFixed()
	{
		var DeltaTime = Time.fixedDeltaTime;
		for (int i = _listLength - 1; i >= 0 ; --i)
		{
			_listController[i].TickAI(DeltaTime);
		}
		for (int i = _listLength - 1; i >= 0; --i)
		{
			_listController[i].TickMove(DeltaTime);
		}
		for (int i = _listLength - 1; i >= 0; --i)
		{
			_listController[i].TickEntity(DeltaTime);
		}
		for (int i = _listLength - 1; i >= 0; --i)
		{
			_listController[i].TickShoot(DeltaTime);
		}

		_cam.UpdatePosCam();
	}

	public void SubElement(BaseController Element)
	{
		if (_listController.Find(e => Element == e) == null)
		{
			if (_capacity <= _listLength)
			{
				_capacity *= 2;
				_listController.Capacity = _capacity;
			}
			++_listLength;
			_listController.Add(Element);
		}
		else
		{
			Debug.LogWarning("[GameplayLoop/SubElement]: Already sub : " + Element.name);
		}
	}

	public void RemoveElement(BaseController Element)
	{
		var index = _listController.FindIndex(e => Element == e);
		if (index != -1)
		{
			_listController.RemoveAt(index);

			--_listLength;
		}
		else
		{
			Debug.LogError("Remove Element not present" + Element.name);
		}
	}
}
