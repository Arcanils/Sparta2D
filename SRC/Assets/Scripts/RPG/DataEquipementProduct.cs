using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEquipementProduct", menuName = "RPG/DataEquipementProduct")]
public class DataEquipementProduct : ScriptableObject
{
	public List<StatRandomProperty> FlatRandomStats;
	public List<StatRandomProperty> CoefRandomStats;
	public QualityStats[] QualityStatsToSelect;

	public void GenereStats(Equipement.EQuality quality, out RpgStats stats, out string name)
	{
		var indexQuality = (int)quality;
		var flatStatsFinal = new List<Stat>();
		var coefStatsFinal = new List<Stat>();
		for (int i = 0; i <= indexQuality; i++)
		{
			var qualityStat = QualityStatsToSelect[i];
			var statLucks = qualityStat.StatLucks;
			for (int j = qualityStat.NStatsToAdd - 1; j >= 0; --j)
			{
				var indexStatToAdd = SelectIndexInArrayLuck(statLucks);
				var statsToAdd = statLucks[indexStatToAdd].Stat;
				var isAddType = statLucks[indexStatToAdd].AddType == EAddType.ADD;
				if (isAddType)
				{
					var valueStat = FlatRandomStats.FirstOrDefault(item => item.StatSelected == statsToAdd);
					flatStatsFinal.Add(valueStat.Generate());
				}
				else
				{
					var valueStat = CoefRandomStats.FirstOrDefault(item => item.StatSelected == statsToAdd);
					coefStatsFinal.Add(valueStat.Generate());
				}
			}
		}
		stats = new RpgStats(new Stats(flatStatsFinal.ToArray()), new Stats(coefStatsFinal.ToArray()));
		name = " NAME_TODO " + quality.ToString();
	}

	public static int SelectIndexInArrayLuck(StatLuck[] statsLuck)
	{
		var statsCumul = new int[statsLuck.Length];
		var total = 0;
		for (int i = 0, iLength = statsCumul.Length; i < iLength; i++)
		{
			total += statsLuck[i].Luck;
			statsCumul[i] = total;
		}

		var randomValue = UnityEngine.Random.Range(0, total);
		for (int i = 0, iLength = statsCumul.Length; i < iLength; i++)
		{
			if (randomValue < statsCumul[i])
				return i;
		}

		return statsLuck.Length - 1;
	}
}


[System.Serializable]
public struct StatRandomProperty
{
	public Stat.EBaseStats StatSelected;

	public float MinValue;
	public float MaxValue;

	public Stat Generate()
	{
		var randomValue = UnityEngine.Random.Range(MinValue, MaxValue);
		Debug.LogWarning(StatSelected + " " + MinValue + " " + randomValue + MaxValue);
		return new Stat((int)StatSelected, randomValue);
	}
}