using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;


[Serializable]
public struct RpgStats
{
	public Stats FlatStats;
	public Stats CoefStats;

	public RpgStats(Stats flatStats, Stats coefStats)
	{
		FlatStats = flatStats;
		CoefStats = coefStats;
	}

	public string ToString(string[] namesStats)
	{
		return FlatStats.ToString(namesStats) + "\n" + CoefStats.ToString(namesStats);
	}
}

[Serializable]
public struct Stats
{
	public Stat[] Values;

	public Stats(Stat[] stats)
	{
		Values = stats;
	}

	public void AddStats(Stats otherStats)
	{
		for (int i = otherStats.Values.Length - 1; i >= 0; --i)
		{
			AddBaseStat(otherStats.Values[i].Key, otherStats.Values[i].Value);
		}
	}

	public void AddBaseStat(int indexStat, float value)
	{
		for (int i = Values.Length - 1; i >= 0; --i)
		{
			if (Values[i].Key != indexStat)
				continue;

			Values[indexStat].Value += value;
			return;
		}
	}
	public void MultiplyStat(Stat stat)
	{
		MultiplyStat(stat.Key, stat.Value);
	}
	public void MultiplyStat(int indexStat, float value)
	{
		for (int i = Values.Length - 1; i >= 0; --i)
		{
			if (Values[i].Key != indexStat)
				continue;

			Values[indexStat].Value *= value;
			return;
		}
	}
	public string ToString(string[] namesStats = null)
	{ 
		var txt = new StringBuilder();
		for (int i = 0, iLength = Values.Length; i < iLength; i++)
		{
			txt.Append((namesStats != null && namesStats.Length > i ? namesStats[Values[i].Key] : i.ToString()) + " : " + Values[i].Value + "\n");
		}

		return txt.ToString();
	}

	public void SetStats(int indexStat, float value)
	{
		for (int i = Values.Length - 1; i >= 0; --i)
		{
			if (Values[i].Key != indexStat)
				continue;

			Values[indexStat].Value = value;
			return;
		}
	}


	public static Stats EmptyStats(float defaultValue = 0f)
	{
		var stats = new Stat[Enum.GetNames(typeof(Stat.EBaseStats)).Length];
		for (int i = stats.Length - 1; i >= 0; --i)
		{
			stats[i] = new Stat(i, defaultValue);
		}

		return new Stats(stats);
	}


}


public enum EAddType
{
	ADD,
	MULTIPLI,
}

[Serializable]
public struct Stat
{
	public enum EBaseStats
	{
		STRENGTH,
		DEXTERITY,
		VITALITY,
		WISDOM,
		LUCK,
		PHYSICS_RES,
		PHYSICS_DMG ,
		MAGIC_RES,
		MAGIC_DMG,
		COEF_CRIT,
		COEF_CRIT_DMG_MULTIPLIER,
		ATTACK_SPEED,
		COOLDOWN_REDUCTION,
		SPEED,
		DROP_RATE,
		HP,
	}
	public int Key;
	public float Value;

	public Stat(int key, float value)
	{
		Key = key;
		Value = value;
	}
}

[Serializable]
public struct StatClamp
{
	public Stat Value;
	public float Min;
	public float Max;
}

[Serializable]
public class EntityDataConfig
{
	public Stats BaseStats;
	public Stats GainBaseStatsByLvl;

	public Stats GetStats()
	{
		return BaseStats;
	}
}

[Serializable]
public class Entity
{
	public EntityDataConfig EntityData;
	public EntityEquipement Equipements;
	public BuffStats[] Buffs;
	public Stats CurrentStats;

	public float PhysicsDMG
	{
		get { return GetData(Stat.EBaseStats.PHYSICS_DMG); }
	}
	public float MagicDMG
	{
		get { return GetData(Stat.EBaseStats.MAGIC_DMG); }
	}
	public float PhysicsRes
	{
		get { return GetData(Stat.EBaseStats.PHYSICS_RES); }
	}
	public float MagicRes
	{
		get { return GetData(Stat.EBaseStats.MAGIC_RES); }
	}
	public float HP
	{
		get { return GetData(Stat.EBaseStats.HP); }
	}

	//CalculStatsFinal;
	//CalculStatsFinalWithoutBuff
	//TickBuff
	//Private / getter setter
	//Get Damage Physique / Magic
	// Inflict Damage 
	// Get Current HP

	public void CalculFinalStats()
	{
		CurrentStats = Stats.EmptyStats();
		var baseEntityStats = EntityData.GetStats();
		var equipementStats = Equipements.GetStats();

		CurrentStats.AddStats(baseEntityStats);
		CurrentStats.AddStats(equipementStats.FlatStats);

		var coefStats = Stats.EmptyStats(1f);
		coefStats.AddStats(equipementStats.CoefStats);


		for (int i = (int)Stat.EBaseStats.LUCK; i >= 0; --i)
		{
			CurrentStats.MultiplyStat(coefStats.Values[i]);
		}

		CurrentStats.AddBaseStat((int)Stat.EBaseStats.PHYSICS_RES, GetData(Stat.EBaseStats.STRENGTH) * COEF_STR_PHYS_RES);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.PHYSICS_DMG, GetData(Stat.EBaseStats.STRENGTH) * COEF_STR_PHYS_DMG);

		CurrentStats.AddBaseStat((int)Stat.EBaseStats.ATTACK_SPEED, GetData(Stat.EBaseStats.DEXTERITY) * COEF_DEX_AS);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.SPEED, GetData(Stat.EBaseStats.DEXTERITY) * COEF_DEX_SPEED);

		CurrentStats.AddBaseStat((int)Stat.EBaseStats.HP, GetData(Stat.EBaseStats.VITALITY) * COEF_VIT_HP);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.MAGIC_RES, GetData(Stat.EBaseStats.VITALITY) * COEF_VIT_PHYS_RES);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.PHYSICS_RES, GetData(Stat.EBaseStats.VITALITY) * COEF_VIT_MAGIC_RES);

		CurrentStats.AddBaseStat((int)Stat.EBaseStats.MAGIC_RES, GetData(Stat.EBaseStats.WISDOM) * COEF_WIS_MAGIC_RES);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.MAGIC_DMG, GetData(Stat.EBaseStats.WISDOM) * COEF_WIS_MAGIC_DMG);


		CurrentStats.AddBaseStat((int)Stat.EBaseStats.DROP_RATE, GetData(Stat.EBaseStats.LUCK) * COEF_LUC_DROP_RATE);
		CurrentStats.AddBaseStat((int)Stat.EBaseStats.COEF_CRIT, GetData(Stat.EBaseStats.LUCK) * COEF_LUC_CRIT);

		for (int i = (int)Stat.EBaseStats.LUCK + 1, iLength = (int)Stat.EBaseStats.HP + 1; i < iLength; ++i)
		{
			CurrentStats.MultiplyStat(coefStats.Values[i]);
		}

	}
	private const float COEF_STR_PHYS_RES = 0.2f;
	private const float COEF_STR_PHYS_DMG = 1f;

	private const float COEF_DEX_AS = 1f;
	private const float COEF_DEX_SPEED = 1f;

	private const float COEF_VIT_HP = 5f;
	private const float COEF_VIT_PHYS_RES = 0.1f;
	private const float COEF_VIT_MAGIC_RES = 0.1f;

	private const float COEF_WIS_MAGIC_RES = 0.2f;
	private const float COEF_WIS_MAGIC_DMG = 1f;

	private const float COEF_LUC_DROP_RATE = 0.5f;
	private const float COEF_LUC_CRIT = 0.5f;

	/*
		STRENGTH,
		DEXTERITY,
		VITALITY,
		WISDOM,
		LUCK,
		PHYSICS_RES,
		PHYSICS_DMG ,
		MAGIC_RES,
		MAGIC_DMG,
		COEF_CRIT,
		COEF_CRIT_DMG_MULTIPLIER,
		ATTACK_SPEED,
		COOLDOWN_REDUCTION,
		SPEED,
		DROP_RATE,
		HP,
		*/
	private float GetData(Stat.EBaseStats typeData)
	{
		var value = (int)typeData;
		return CurrentStats.Values.FirstOrDefault(data => data.Key == value).Value;
	}
}

[Serializable]
public class BuffStats
{
	public enum EBuff
	{
		BONUS,
		MALUS,
	}

	public string NameBuff;
	public EBuff TypeBuff;
	public RpgStats Stats;

	public float Duration;
}

[Serializable]
public class EntityEquipement
{
	public Equipement Head;
	public Equipement Chest;
	public Equipement Legs;
	public Equipement Ring1;
	public Equipement Ring2;
	public Equipement RightHand;
	public Equipement LeftHand;

	public RpgStats GetStats()
	{
		List<Stat> baseStats = new List<Stat>();
		List<Stat> coefStats = new List<Stat>();
		if (Head != null)
			Head.GetStats(baseStats, coefStats);
		if (Chest != null)
			Chest.GetStats(baseStats, coefStats);
		if (Legs != null)
			Legs.GetStats(baseStats, coefStats);
		if (Ring1 != null)
			Ring1.GetStats(baseStats, coefStats);
		if (Ring2 != null)
			Ring2.GetStats(baseStats, coefStats);
		if (RightHand != null)
			RightHand.GetStats(baseStats, coefStats);
		if (LeftHand != null)
			LeftHand.GetStats(baseStats, coefStats);

		return new RpgStats(new Stats(baseStats.ToArray()), new Stats(coefStats.ToArray()));
	}
}

public class Equipement
{
	public enum EEquipement
	{
		HEAD,
		CHEST,
		LEGS,
		RING,
		ONE_HAND_WEAPON,
		SHIELD,
		TWO_HAND_WEAPON,
		ORB,
	}
	public enum EQuality
	{
		COMMON,
		UNCOMMON,
		RARE,
		EPIQUE,
		LEGENDARY,
	}
	public string Name;
	public EEquipement Type;
	public EQuality Quality;
	public RpgStats Stats;

	public void GetStats(List<Stat> flatValues, List<Stat> coefStats)
	{
		flatValues.AddRange(Stats.FlatStats.Values);
		coefStats.AddRange(Stats.CoefStats.Values);
	}
}
// BaseStatsFinal = BaseStats + BonusFlatBaseStats + BaseStats * BonusCoefBaseStats
// ItemsStatsFinal = Foreach ( itemStats)
// GlobalStatsFinal = BaseStatsFinal + ItemStatsFinal + FlatBonus + (BaseStatsFinal + ItemStatsFinal) * BonusCoefGlobalStats;


//Str => Dmg phy, Resistance physique
//Dex => AS,
//Vit => HP, Res physique, Res Elemen global,
//Wis => Dmg spell
//Luck

// Armor
// ResElemental
// % CoefCritDmg
// Speed
// Cooldown
// Dmg Phys
// Dmg Magic
// % Crit
// Attaqck speed / cast speed
// HP
//CoefReductionDmg = StatsRes Evaluate sur Curve
// DamageReceive = (FlatDamage  - FlatReduction ) * (1 - CoefReduction Damage)
																			  