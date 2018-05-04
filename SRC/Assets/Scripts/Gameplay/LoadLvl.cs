using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderLvl
{
	private EntityFactory _entityFactory;

	public LoaderLvl(EntityFactory Factory)
	{
		Init(Factory);
	}

	public void Init(EntityFactory Factory)
	{
		_entityFactory = Factory;
	}
	/*
	public void CreateLvl(LvlConfig Config)
	{
		_entityFactory.GetNewEntity(Config.Player, new Vector3(-6f, 0f, 0f));
	}*/

}

