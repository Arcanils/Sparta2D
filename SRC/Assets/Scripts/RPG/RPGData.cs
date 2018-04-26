using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using UnityEngine;


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

	public string ToString(string[] namesStats = null)
	{ 
		var txt = new StringBuilder();
		for (int i = 0, iLength = Values.Length; i < iLength; i++)
		{
			txt.Append((namesStats != null && namesStats.Length > i ? namesStats[Values[i].Key] : i.ToString()) + " : " + Values[i].Value + "\n");
		}

		return txt.ToString();
	}
}


public enum EAddType
{
	ADD,
	MULTIPLI,
}

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

public struct StatClamp
{
	public Stat Value;
	public float Min;
	public float Max;
}

public class EntityConfig
{
	public Stats BaseStats;
	public Stats GainBaseStatsByLvl;
}

public class Entity
{
	public EntityConfig EntityData;
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

	//CalculStatsFinal;
	//CalculStatsFinalWithoutBuff
	//TickBuff
	//Private / getter setter
	//Get Damage Physique / Magic
	// Inflict Damage 
	// Get Current HP

	private float GetData(Stat.EBaseStats typeData)
	{
		var value = (int)typeData;
		return CurrentStats.Values.FirstOrDefault(data => data.Key == value).Value;
	}
}

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

public class EntityEquipement
{
	public Equipement Head;
	public Equipement Chest;
	public Equipement Legs;
	public Equipement Ring1;
	public Equipement Ring2;
	public Equipement RightHand;
	public Equipement LeftHand;
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