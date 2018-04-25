using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipementGenerator", menuName = "RPG/EquipementGenerator")]
public class EquipementGenerator : ScriptableObject
{
	public EquipementGeneratorDatas[] Datas;

	public void GenerateRandomEquipement()
	{
		var dataEquipement = Datas[UnityEngine.Random.Range(0, Datas.Length)];

		RpgStats stats;
		string name;

		dataEquipement.GenereRandom(out stats, out name);


		var str = name + "\n" + stats.ToString(Enum.GetNames(typeof(Stat.EBaseStats)));

		Debug.Log(str);
	}
}
