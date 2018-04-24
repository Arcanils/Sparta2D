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

		RpgStats flatStats;
		RpgStats coefStats;
		string name;

		dataEquipement.GenereRandom(out flatStats, out coefStats, out name);


		var str = name + "\n" + flatStats.ToString() + "\n" + coefStats.ToString();

		Debug.Log(str);
	}
}
