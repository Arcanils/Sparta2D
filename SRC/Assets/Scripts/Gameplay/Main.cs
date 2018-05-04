using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
	public static Main Instance { get; private set; }

	public EntityConfig Player;
	public Transform SpawnPlayer;

	public LvlConfig[] LvlConfigs;

	[SerializeField]
	private GameCamera PrefabCam;

	public GameCamera GameCameraInstance { get; private set; }
	public PoolManager PoolManagerInstance { get; private set; }
	public GameplayLoop GameplayLoopInstance { get; private set; }
	public EntityFactory EntityFactoryInstance { get; private set; }

	private LoaderLvl _loadLvl;

	public void Awake()
	{
		InitPoolManager();
		InitCamera();
		InitGameplayLoop();
		InitEntityFactory();
		InitLoaderLvl();
		Instance = this;
	}

	public void Start()
	{
		StartCoroutine(MainGameplayEnum());
	}

	private void InitPoolManager()
	{
		var goContainer = new GameObject("[PoolContainer]");
		var transContainer = goContainer.transform;
		PoolManagerInstance = new PoolManager(transContainer);
	}
	
	private void InitCamera()
	{
		GameCameraInstance = GameObject.Instantiate<GameCamera>(PrefabCam);
	}

	private void InitGameplayLoop()
	{
		GameplayLoopInstance = new GameplayLoop(GameCameraInstance);
	}
	private void InitEntityFactory()
	{
		EntityFactoryInstance = new EntityFactory(PoolManagerInstance);
	}

	private void InitLoaderLvl()
	{
		_loadLvl = new LoaderLvl(EntityFactoryInstance);
	}

	private IEnumerator MainGameplayEnum()
	{
		StartCoroutine(MainGameplayFixedEnum());
		StartCoroutine(MainGameplayNormalEnum());
		yield return new WaitForSeconds(0.1f);
		SpawnGame();
		//_loadLvl.StartRound();
	}

	private IEnumerator MainGameplayFixedEnum()
	{
		while (true)
		{
			GameplayLoopInstance.TickFixed();
			yield return new WaitForFixedUpdate();
		}
	}
	private IEnumerator MainGameplayNormalEnum()
	{
		while (true)
		{
			GameplayLoopInstance.Tick();
			yield return null;
		}
	}

	private void SpawnGame()
	{
		PawnComponent pawnPlayer;
		AbstractController controller;
		EntityFactoryInstance.GetNewEntity(Player, SpawnPlayer.position, out pawnPlayer, out controller);
		GameCameraInstance.Init(pawnPlayer.transform);
	}

	private void RespawnPlayer()
	{
		//EntityFactoryInstance.GetNewEntity(LvlConfigs[0].Player, new Vector3(-6f, 0f, 0f));
	}
	
}

public class RoundBehaviour
{
	public Transform[] SpawnPosition;

	private IUiRoundBehaviour _iUIBehaviour;

	public void Init(IUiRoundBehaviour iUIBehaviour)
	{
		_iUIBehaviour = iUIBehaviour;
	}

	public void StartNewRound()
	{

	}

	public void ClearRound()
	{

	}

	public void OnRoundComplete()
	{

	}

	public void OnRoundFailed()
	{

	}

	public IEnumerator StartNewRoundEnum()
	{
		ClearRound();
		//Print NextRound
		yield return null;
	}

	public IEnumerator OnRoundCompleteEnum()
	{
		//PrintCurrent lvl success
		//Print NextRound
		//Spawn
		yield return null;
	}

	public IEnumerator OnRoundFailedEnum()
	{
		//PrintCurrent lvl failure
		//Show loot / Equipement Phase
		//Skill Tree, Skill switch
		//Wait input to restart
		yield return null;
	}
}

public interface IUiRoundBehaviour
{
	IEnumerator ShowCurrentLvlAnimEnum(bool success);
	IEnumerator ShowNextLvlAnimEnum();
	IEnumerator ShowLootPhaseEnum();
	IEnumerator ShowSkillPhaseEnum();
	IEnumerator ShowRestart();
}
