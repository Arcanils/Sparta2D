using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGData
{
	public float Strength;
	public float Dexterity;
	public float Vitality;
	public float Wisdom;
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