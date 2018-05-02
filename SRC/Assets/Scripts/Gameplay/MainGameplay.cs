using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameplay : MonoBehaviour {


	public static MainGameplay Instance;

	//Factory
	//Pool
	//GDSelectLVL
	//UIManager Endlvl
	//UIManager Skilltree


	public void Awake()
	{
		Instance = this;
	}


	public void InitLevel()
	{

	}

	public void CreatePlayer()
	{

	}

	public void CreateEnemy(int lvl)
	{

	}



}

public interface IMainControl
{
	void PauseGame();
	void ShowEndScreenBattle();
	void ShowSkillTree();
}

public class UIBehaviour
{
	public GameObject MenuPause;

	public void SetActivePause(bool enable)
	{
		MenuPause.SetActive(enable);
		Time.timeScale = enable ? 0f : 1f;
	}
}

public class RoundBehaviour : IEnemyDeath
{
	private List<IEntityPool> _entityRound;
	//Factory

	public void SpawnRound()
	{
		//Data gd to spawn
	}

	void IEnemyDeath.OnDeath(IEntityPool entity)
	{
		_entityRound.Remove(entity);
	}
}

public interface IEntityPool
{

}

public interface IEnemyDeath
{
	void OnDeath(IEntityPool entity);
}