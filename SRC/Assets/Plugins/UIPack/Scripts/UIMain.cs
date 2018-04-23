using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMain : MonoBehaviour
{
	public static int GlobalParamSet;
	

	public void Play(int index)
	{
		GlobalParamSet = index;
		SceneManager.LoadScene(1);
	}

	public void Quit()
	{
		Application.Quit();
	}
}
