using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class MenuAnimation : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
	private Vector3 initialScale;

	[SerializeField]
	private float animationDuration = 0.2f;

	private void Start()
	{
		initialScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
	}

	public void OnPointerEnter(PointerEventData eventData)
	{
		transform.DOScale(initialScale * 1.2f, animationDuration);
	}

	public void OnPointerExit(PointerEventData eventData)
	{
		transform.DOScale(initialScale, animationDuration);
	}
}
