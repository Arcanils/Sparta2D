using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipementGeneratorDatas", menuName = "RPG/EquipementGeneratorDatas")]
public class EquipementGeneratorDatas : ScriptableObject
{
	public Equipement.EEquipement TypeEquipement;
	public Sprite Icon;
	public string Name;
	public DataEquipementProduct Datas;

	public void GenereRandom(out RpgStats stats, out string name)
	{
		Datas.GenereStats((Equipement.EQuality)Random.Range(0, System.Enum.GetNames(typeof(Equipement.EQuality)).Length), out stats, out name);
		name = Name + " " + name;
	}
}

[System.Serializable]
public struct QualityStats
{
	public Equipement.EQuality Quality;
	public int NStatsToAdd;
	public StatLuck[] StatLucks;
}

[System.Serializable]
public struct StatLuck
{
	public Stat.EBaseStats Stat;
	public EAddType AddType;
	[Range(0,10000)]
	public int Luck;
}