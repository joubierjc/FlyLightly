using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class WritingText : MonoBehaviour {

	public Text firstTextToRead;
	public float writingDuration = 10f;

	public Text button;

	private string endText;

	private void Start()
	{
		endText = firstTextToRead.text;
		firstTextToRead.text = "";
		firstTextToRead.DOText(endText.ToUpper(), writingDuration).OnComplete(() => {
			button.text = "PLAY";
		});
	}
}
