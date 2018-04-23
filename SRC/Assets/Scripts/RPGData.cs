using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct RpgStats
{
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
	public float CastSpeed;
	public float CoefCooldown;
	public float DmgPhysique;
	public float DmgMagique;
	public float HP;
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