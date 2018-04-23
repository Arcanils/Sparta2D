using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIMovePanel : MonoBehaviour {

	public DataPanel[] Datas;

	public int CurrentState;

	public void Awake()
	{
		for (int i = 0; i < Datas.Length; i++)
		{
			Datas[i].Init(CurrentState == i);
		}
	}

	public void NextState(int indexState)
	{
		if (CurrentState == indexState)
			return;
		StartCoroutine(PlayAnimEnum(indexState));
	}

	private IEnumerator PlayAnimEnum(int indexState)
	{
		if (CurrentState >= 0 && CurrentState < Datas.Length)
		{
			yield return Datas[CurrentState].MoveEnum(false);
		}
		CurrentState = indexState;
		if (CurrentState >= 0 && CurrentState < Datas.Length)
		{
			yield return Datas[CurrentState].MoveEnum(true);
		}
	}
}

[System.Serializable]
public class DataPanel
{
	public enum EDirectionFrom
	{
		LEFT,
		RIGHT,
		TOP,
		BOTTOM,
	}
#if UNITY_EDITOR
	public string NameState;
#endif
	public RectTransform ContainerToMove;
	public EDirectionFrom DirectionFrom;
	public float Duration = 1f;
	public AnimationCurve Curve = AnimationCurve.EaseInOut(0f, 0f, 1f, 1f);
	public UnityEvent ActionOnFinish; 
	private Vector2 _origin;
	private CanvasGroup _canvasGroup;
	private Vector2 _resolution;

	public void Init(bool isFocus)
	{
		_origin = ContainerToMove.anchoredPosition;
		_canvasGroup = ContainerToMove.GetComponent<CanvasGroup>() ?? ContainerToMove.gameObject.AddComponent<CanvasGroup>();
		_resolution = ContainerToMove.GetComponentInParent<CanvasScaler>().referenceResolution;
		if (isFocus)
		{
			ContainerToMove.gameObject.SetActive(true);
			_canvasGroup.interactable = true;
		}
		else
		{
			ContainerToMove.anchoredPosition += GetOffsetScreen(DirectionFrom);
			ContainerToMove.gameObject.SetActive(false);
			_canvasGroup.interactable = false;
		}
	}

	public IEnumerator MoveEnum(bool isGoingIn)
	{
		return isGoingIn ? MoveIn() : MoveOut();
	}

	private IEnumerator MoveOut()
	{
		_canvasGroup.interactable = false;

		var beg = ContainerToMove.anchoredPosition;
		var end = _origin + GetOffsetScreen(DirectionFrom);
		var routine = HelperTween.MoveRectTransformEnum(
			   ContainerToMove,
			   beg,
			   end,
			   Duration,
			   Curve,
			   true,
			   () =>
			   {
				   ContainerToMove.gameObject.SetActive(false);
				   if (ActionOnFinish != null)
					   ActionOnFinish.Invoke();
			   }
		);

		return routine;
	}

	private IEnumerator MoveIn()
	{
		ContainerToMove.gameObject.SetActive(true);
		var beg = ContainerToMove.anchoredPosition;
		var end = _origin;
		var routine = HelperTween.MoveRectTransformEnum(
			   ContainerToMove,
			   beg,
			   end,
			   Duration,
			   Curve,
			   true,
			   () => _canvasGroup.interactable = true);
		return routine;
	}

	private Vector2 GetOffsetScreen(EDirectionFrom direction)
	{
		switch (direction)
		{
			case EDirectionFrom.LEFT:
				return new Vector2(-_resolution.x, 0f);
			case EDirectionFrom.RIGHT:
				return new Vector2(_resolution.x, 0f);
			case EDirectionFrom.TOP:
				return new Vector2(0f, _resolution.y);
			case EDirectionFrom.BOTTOM:
				return new Vector2(0f , -_resolution.y);
		}

		return Vector2.zero;
	}
}
