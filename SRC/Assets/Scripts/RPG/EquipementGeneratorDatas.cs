using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipementGeneratorDatas", menuName = "RPG/EquipementGeneratorDatas")]
public class EquipementGeneratorDatas : ScriptableObject
{
	public Equipement.EEquipement TypeEquipement;
	public Sprite Icon;
	public string Name;
	public DataEquipementProduct[] Datas;

	public void GenereRandom(out RpgStats flatStats, out RpgStats coefStats, out string name)
	{
		var subData = Datas[UnityEngine.Random.Range(0, Datas.Length)];
		subData.GenereStats(out flatStats, out coefStats, out name);
		name = Name + " " + name;
	}
}
