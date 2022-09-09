using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidableMenu : MonoBehaviour {

	[SerializeField] private CanvasGroup canvas;

	private bool isShowing = false;
	public bool IsShowing {
		get => isShowing;
		set {
			if (value != isShowing) {
				canvas.alpha = value ? 1 : 0;
				canvas.blocksRaycasts = value;
				canvas.interactable = value;
			}

			isShowing = value;
		}
	}

	public void Show() {
		IsShowing = true;
	}

	public void Hide() {
		IsShowing = false;
	}

}
