using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EquipementGenerator))]
public class EquipementGeneratorEditor : Editor {

	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();

		var e = target as EquipementGenerator;

		if (GUILayout.Button("TestGeneration Weapon"))
		{
			e.GenerateRandomEquipement();
		}
	}
}
