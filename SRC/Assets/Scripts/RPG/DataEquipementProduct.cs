using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DataEquipementProduct", menuName = "RPG/DataEquipementProduct")]
public class DataEquipementProduct : ScriptableObject
{
	public Equipement.EQuality Quality;
	public LuckChooseSetOfStats[] Probs;

	public void GenereStats(out RpgStats flatStats, out RpgStats coefStats, out string name)
	{
		var totalAmount = 0;
		for (int i = 0; i < Probs.Length; i++)
		{
			totalAmount += Probs[i].LuckToBeTheChosenOne;
		}

		var randomValue = UnityEngine.Random.Range(0, totalAmount);

		var selected = -1;
		var total = 0;
		for (int i = 0; i < Probs.Length; i++)
		{
			total += Probs[i].LuckToBeTheChosenOne;

			if (randomValue < total)
			{
				selected = i;
				break;
			}
		}

		if (selected == -1)
		{
			Debug.LogError("FAILED : " + totalAmount + " " + randomValue);
			flatStats = default(RpgStats);
			coefStats = default(RpgStats);
			name = null;
			return;
		}
		Probs[selected].GenereData(out flatStats, out coefStats, out name);
		name += " " + Quality.ToString();
	}
}

[System.Serializable]
public class LuckChooseSetOfStats
{
	public HolderStatsRandom Data;
	[Range(0, 10000)]
	public int LuckToBeTheChosenOne;

	public void GenereData(out RpgStats flatStats, out RpgStats coefStats, out string name)
	{
		Data.Generate(out flatStats, out coefStats, out name);
	}
}
