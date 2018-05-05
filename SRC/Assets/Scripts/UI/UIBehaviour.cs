using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBehaviour : MonoBehaviour, IUiRoundBehaviour
{
	public AnimationCurve CurveAnim;
	public UIDisplayEntityStats UIEntityStats;
	public float DurationAnimText = 2f;
	//Loot
	//SkillTree
	//SkillExchange

	public RectTransform ContainerText;
	public Text TextRound;

	private IUIBehaviourUtils _iUiBehaviourUtils;

	public void Awake()
	{
		ContainerText.anchoredPosition = new Vector2((ContainerText.root as RectTransform).rect.width, 0f);
		ContainerText.gameObject.SetActive(false);
	}

	public void Init(IUIBehaviourUtils iUiBehaviourUtils)
	{
		_iUiBehaviourUtils = iUiBehaviourUtils;
	}

	private IEnumerator MoveText(string msg)
	{
		TextRound.text = msg;
		ContainerText.gameObject.SetActive(true);
		var length = (ContainerText.root as RectTransform).rect.width;
		var beg = new Vector2(-length, 0f);

		yield return HelperTween.MoveRectTransformEnum(
			ContainerText,
			beg,
			beg + new Vector2(length * 2f, 0f),
			DurationAnimText, CurveAnim, true);

		ContainerText.gameObject.SetActive(false);
	}

	IEnumerator IUiRoundBehaviour.ShowCurrentLvlAnimEnum()
	{
		var index = _iUiBehaviourUtils.GetCurrentIndexRound();
		return MoveText("Round n°" + index.ToString("00"));
	}

	IEnumerator IUiRoundBehaviour.ShowFailureAnim()
	{
		var index = _iUiBehaviourUtils.GetCurrentIndexRound();
		return MoveText("Round n°" + index.ToString("00") + " failed !");
	}

	IEnumerator IUiRoundBehaviour.ShowLootPhaseEnum()
	{
		yield return new WaitForSeconds(1f);
	}

	IEnumerator IUiRoundBehaviour.ShowRestart()
	{
		yield return null;
	}

	IEnumerator IUiRoundBehaviour.ShowSkillPhaseEnum()
	{
		yield return new WaitForSeconds(1f);
	}

	IEnumerator IUiRoundBehaviour.ShowSuccessAnim()
	{
		var index = _iUiBehaviourUtils.GetCurrentIndexRound();
		return MoveText("Round n°" + index.ToString("00") + " success !");
	}
}

public interface IUIBehaviourUtils
{
	void PauseGame(bool Pause);
	int GetCurrentIndexRound();
}
