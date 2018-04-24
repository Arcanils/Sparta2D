using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "RandomStats", menuName = "RPG/RandomStats")]
public class HolderStatsRandom : ScriptableObject
{
	public string Name;
	public BaseStatRandomProperty[] BaseStatsFlat;
	public BaseStatRandomProperty[] BaseStatsCoef;
	public AdvancedStatRandomProperty[] AdvancedStatsFlat;
	public AdvancedStatRandomProperty[] AdvancedStatsCoef;

	public void Generate(out RpgStats flatStats, out RpgStats coefStats, out string name)
	{
		flatStats = new RpgStats();
		for (int i = BaseStatsFlat.Length - 1; i >= 0; --i)
		{
			var value = BaseStatsFlat[i];
			flatStats.AddStat(value.StatSelected, Random.Range(value.MinValue, value.MaxValue));
		}
		for (int i = AdvancedStatsFlat.Length - 1; i >= 0; --i)
		{
			var value = AdvancedStatsFlat[i];
			flatStats.AddStat(value.StatSelected, Random.Range(value.MinValue, value.MaxValue));
		}

		coefStats = new RpgStats();
		for (int i = BaseStatsCoef.Length - 1; i >= 0; --i)
		{
			var value = BaseStatsCoef[i];
			coefStats.AddStat(value.StatSelected, Random.Range(value.MinValue, value.MaxValue));
		}
		for (int i = AdvancedStatsCoef.Length - 1; i >= 0; --i)
		{
			var value = AdvancedStatsCoef[i];
			coefStats.AddStat(value.StatSelected, Random.Range(value.MinValue, value.MaxValue));
		}

		name = Name;
	}
}

[System.Serializable]
public struct BaseStatRandomProperty
{
	public RpgStats.EBaseStats StatSelected;
	public float MinValue;
	public float MaxValue;
}


[System.Serializable]
public struct AdvancedStatRandomProperty
{
	public RpgStats.EAdvancedStats StatSelected;
	public float MinValue;
	public float MaxValue;
}
