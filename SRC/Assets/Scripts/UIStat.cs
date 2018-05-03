using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIStat : MonoBehaviour
{
	public Text Label;
	public Text Value;

	public void UpdateData(Stat stat)
	{
		Label.text = ((Stat.EBaseStats)stat.Key).ToString();
		Value.text = stat.Value.ToString();
	}
}
