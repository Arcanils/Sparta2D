using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Main : MonoBehaviour , IRoundBehaviour, IUIBehaviourUtils
{
	public static Main Instance { get; private set; }

	public UIBehaviour UiBehaviour;
	public EntityConfig Player;
	public Transform SpawnPlayer;

	[SerializeField]
	private GameCamera PrefabCam;

	public GameCamera GameCameraInstance { get; private set; }
	public PoolManager PoolManagerInstance { get; private set; }
	public GameplayLoop GameplayLoopInstance { get; private set; }
	public EntityFactory EntityFactoryInstance { get; private set; }

	[SerializeField]
	private LvlLoader _loadLvl;

	private RoundBehaviour _roundBehaviour;
	public void Awake()
	{
		InitPoolManager();
		InitCamera();
		InitGameplayLoop();
		InitEntityFactory();
		InitLoaderLvl();
		_roundBehaviour = new RoundBehaviour(UiBehaviour, this, this);
		Instance = this;
		UiBehaviour.Init(this);
	}

	public void Start()
	{
		StartGameplay();
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
		_loadLvl.Init(EntityFactoryInstance);
	}

	private void StartGameplay()
	{
		StartCoroutine(MainGameplayFixedEnum());
		StartCoroutine(MainGameplayNormalEnum());
		_roundBehaviour.StartNewRound();
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

	private void ClearPlayer()
	{

	}

	AbstractController IRoundBehaviour.SpawnPlayer()
	{
		ClearPlayer();

		PawnComponent pawnPlayer;
		AbstractController controller;
		EntityFactoryInstance.GetNewEntity(Player, SpawnPlayer.position, out pawnPlayer, out controller);
		GameCameraInstance.Init(pawnPlayer.transform);

		return controller;
	}

	List<AbstractController> IRoundBehaviour.SpawnLvl(int indexLevel)
	{
		return _loadLvl.LoadLvl(indexLevel);
	}

	void IRoundBehaviour.ClearRound(bool all)
	{
	}

	void IUIBehaviourUtils.PauseGame(bool Pause)
	{
		//PauseLoopGameplay
		//SpawnController Player MENU
	}

	int IUIBehaviourUtils.GetCurrentIndexRound()
	{
		return _roundBehaviour.IndexRound;
	}
}

public class RoundBehaviour
{
	public int IndexRound { get; private set; }

	private IUiRoundBehaviour _iUIBehaviour;
	private MonoBehaviour _runner;
	private IRoundBehaviour _iRoundBehaviour;
	private List<AbstractController> _entitiesAlives;
	private AbstractController _player;

	public RoundBehaviour(IUiRoundBehaviour iUIBehaviour, IRoundBehaviour iRoundBehaviour, MonoBehaviour runner)
	{
		_iUIBehaviour = iUIBehaviour;
		_iRoundBehaviour = iRoundBehaviour;
		_runner = runner;
	}

	public void StartNewRound()
	{
		_runner.StartCoroutine(StartNewRoundEnum());
	}

	private void OnPlayerDeath(AbstractController controller)
	{
		_player.Destroy();

		_player = null;
	}

	private void OnEnemyDeath(AbstractController controller)
	{
		var entity = _entitiesAlives.First(item => item == controller);

		entity.Destroy();

		_entitiesAlives.Remove(entity);
	}

	private void ClearRound(bool success)
	{
		if (!success)
		_iRoundBehaviour.ClearRound(!success);
	}

	private void OnRoundComplete()
	{
		_runner.StartCoroutine(OnRoundCompleteEnum());
	}

	private void OnRoundFailed()
	{
		_runner.StartCoroutine(OnRoundFailedEnum());
	}

	private IEnumerator StartNewRoundEnum()
	{
		ClearRound(false);
		_player = _iRoundBehaviour.SpawnPlayer();
		IndexRound = 0;

		yield return _iUIBehaviour.ShowRestart();
		yield return _iUIBehaviour.ShowCurrentLvlAnimEnum();
		_entitiesAlives = _iRoundBehaviour.SpawnLvl(IndexRound);

		yield return null;
	}

	private IEnumerator OnRoundCompleteEnum()
	{
		ClearRound(true);

		yield return _iUIBehaviour.ShowSuccessAnim();
		++IndexRound;
		yield return _iUIBehaviour.ShowCurrentLvlAnimEnum();
		_entitiesAlives =_iRoundBehaviour.SpawnLvl(IndexRound);

		yield return null;
	}

	private IEnumerator OnRoundFailedEnum()
	{
		yield return _iUIBehaviour.ShowFailureAnim();
		yield return _iUIBehaviour.ShowLootPhaseEnum();
		yield return _iUIBehaviour.ShowSkillPhaseEnum();
		_runner.StartCoroutine(StartNewRoundEnum());
	}

}

public interface IUiRoundBehaviour
{
	IEnumerator ShowCurrentLvlAnimEnum();
	IEnumerator ShowSuccessAnim();
	IEnumerator ShowFailureAnim();
	IEnumerator ShowLootPhaseEnum();
	IEnumerator ShowSkillPhaseEnum();
	IEnumerator ShowRestart();
}

public interface IRoundBehaviour
{
	AbstractController SpawnPlayer();
	List<AbstractController> SpawnLvl(int indexLvl);
	void ClearRound(bool all);
}
