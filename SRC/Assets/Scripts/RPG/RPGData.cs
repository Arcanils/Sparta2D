using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RpgStats
{
	[System.Flags]
	public enum EBaseStats
	{
		STRENGTH = 1,
		DEXTERITY = 2,
		VITALITY = 4,
		WISDOM = 8,
		LUCK = 16,
	}

	[System.Flags]
	public enum EAdvancedStats
	{
		PHYSICS_RES = 1,
		PHYSICS_DMG = 2,
		MAGIC_RES = 4,
		MAGIC_DMG = 8,
		COEF_CRIT = 16,
		COEF_CRIT_DMG_MULTIPLIER = 32,
		ATTACK_SPEED = 64,
		COOLDOWN_REDUCTION = 128,
		SPEED = 256,
		DROP_RATE = 512,
		HP = 1024,
	}

	public enum EAddType
	{
		ADD,
		MULTIPLI,
	}

	public float Strength;
	public float Dexterity;
	public float Vitality;
	public float Wisdom;
	public float Luck;

	public float PhysicRes;
	public float MagicRes;
	public float Crit;
	public float CritDmgMultiplier;
	public float Speed;
	public float AttackSpeed;
	public float CoefCooldown;
	public float DmgPhysique;
	public float DmgMagique;
	public float HP;
	public float DropRate;

	public void AddStat(EBaseStats statSelected, float value)
	{
		switch (statSelected)
		{
			case EBaseStats.STRENGTH:
				Strength += value;
				break;
			case EBaseStats.DEXTERITY:
				Dexterity += value;
				break;
			case EBaseStats.VITALITY:
				Vitality += value;
				break;
			case EBaseStats.WISDOM:
				Wisdom += value;
				break;
			case EBaseStats.LUCK:
				Luck += value;
				break;
			default:
				break;
		}
		Debug.LogWarning(ToString());
	}
	public void AddStat(EAdvancedStats statSelected, float value)
	{
		switch (statSelected)
		{
			case EAdvancedStats.PHYSICS_RES:
				PhysicRes += value;
				break;
			case EAdvancedStats.PHYSICS_DMG:
				DmgPhysique += value;
				break;
			case EAdvancedStats.MAGIC_RES:
				MagicRes += value;
				break;
			case EAdvancedStats.MAGIC_DMG:
				DmgMagique += value;
				break;
			case EAdvancedStats.COEF_CRIT:
				Crit += value;
				break;
			case EAdvancedStats.COEF_CRIT_DMG_MULTIPLIER:
				CritDmgMultiplier += value;
				break;
			case EAdvancedStats.ATTACK_SPEED:
				AttackSpeed += value;
				break;
			case EAdvancedStats.COOLDOWN_REDUCTION:
				CoefCooldown += value;
				break;
			case EAdvancedStats.SPEED:
				Speed += value;
				break;
			case EAdvancedStats.DROP_RATE:
				DropRate += value;
				break;
			case EAdvancedStats.HP:
				HP += value;
				break;
		}

		Debug.LogWarning(ToString());
	}

	public override string ToString()
	{
		return "Strength  : " + Strength + "\n" +
			"Dexterity  : " + Dexterity + "\n" +
			"Vitality  : " + Vitality + "\n" +
			"Wisdom  : " + Wisdom + "\n" +
			"Luck  : " + Luck + "\n" +
			"PhysicRes  : " + PhysicRes + "\n" +
			"MagicRes  : " + MagicRes + "\n" +
			"Crit  : " + Crit + "\n" +
			"CritDmgMultiplier  : " + CritDmgMultiplier + "\n" +
			"Speed  : " + Speed + "\n" +
			"AttackSpeed  : " + AttackSpeed + "\n" +
			"CoefCooldown  : " + CoefCooldown + "\n" +
			"DmgPhysique  : " + DmgPhysique + "\n" +
			"DmgMagique  : " + DmgMagique + "\n" +
			"HP  : " + HP + "\n" +
			"DropRate  : " + DropRate + "\n";
	}
}

public class EntityConfig
{
	public RpgStats BaseStats;
	public RpgStats GainBaseStatsByLvl;
}

public class Entity
{
	public EntityConfig EntityData;
	public EntityEquipement Equipements;
	public BuffStats[] Buffs;
	public RpgStats CurrentStats;

	//CalculStatsFinal;
	//CalculStatsFinalWithoutBuff
	//TickBuff
	//Private / getter setter
	//Get Damage Physique / Magic
	// Inflict Damage 
	// Get Current HP
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
	public RpgStats BaseStats;
	public RpgStats CoefStats;

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
	public RpgStats BaseStats;
	public RpgStats CoefBonus;
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