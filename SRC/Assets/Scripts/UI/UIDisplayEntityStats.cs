using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIDisplayEntityStats : MonoBehaviour {

	public Sprite[] IconEquipements;
	public Color[] ColorEquipements =
	{
		Color.white,
		Color.green,
		Color.blue,
		Color.magenta,
		Color.red,
	};
	public GameObject PrefabStats;
	public RectTransform ContainerStats;

	public Image IconHead;
	public Image IconChest;
	public Image IconLegs;
	public Image IconRightHand;
	public Image IconLeftHand;
	public Image IconRing_01;
	public Image IconRing_02;


	public GameObject ContainerObject;

	public void GenerateUI(Stats stats, EntityEquipement equipements)
	{
		UpdateStats(stats);
		UpdateEquipement(equipements);

		ContainerObject.SetActive(true);
	}

	private void UpdateStats(Stats stats)
	{
		var values = stats.Values;
		for (int i = 0; i < values.Length; i++)
		{
			var instance = GameObject.Instantiate<GameObject>(PrefabStats, ContainerStats, false);
			var script = instance.GetComponent<UIStat>();
			if (script != null)
				script.UpdateData(values[i]);
		}
	}

	private void UpdateEquipement(EntityEquipement equipements)
	{
		SetIconEquipement(IconHead, equipements.Head);
		SetIconEquipement(IconChest, equipements.Chest);
		SetIconEquipement(IconLegs, equipements.Legs);
		SetIconEquipement(IconRightHand, equipements.RightHand);
		SetIconEquipement(IconLeftHand, equipements.LeftHand);
		SetIconEquipement(IconRing_01, equipements.Ring1);
		SetIconEquipement(IconRing_02, equipements.Ring2);
	}

	private void SetIconEquipement(Image targetIcon, Equipement currentEquipement)
	{
		if (currentEquipement == null)
		{
			targetIcon.overrideSprite = null;
			targetIcon.color = Color.white;
			return;
		}

		targetIcon.overrideSprite = IconEquipements[(int)currentEquipement.Type];
		targetIcon.color = ColorEquipements[(int)currentEquipement.Quality];
	}
}
