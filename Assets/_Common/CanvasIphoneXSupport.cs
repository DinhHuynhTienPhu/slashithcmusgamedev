using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasIphoneXSupport : MonoBehaviour {
	public CanvasScaler canvasScaler;
	public RectTransform panelSafeArea;
	IEnumerator Start ()
	{
		if (canvasScaler != null) {
			float ratio = (float)Screen.width / (float)Screen.height;
			// Iphone 5,6,7,8 - 9/16 = 0.5625f
			// Samsung s8 - 9/18.5 = 0.486
			// Iphone X - 9/19.5
			if (ratio < 0.56f) {
				canvasScaler.matchWidthOrHeight = 0;
			} else {
				canvasScaler.matchWidthOrHeight = 1;
			}
		}
		yield return null;
		UpdateSafeArea ();
	}

	void UpdateSafeArea()
	{
		if (panelSafeArea != null) {
			float ratio = (float)Screen.width / (float)Screen.height;
			bool isIphoneX = Application.platform == RuntimePlatform.IPhonePlayer && ratio < 0.56f;
			#if UNITY_EDITOR
			isIphoneX = ratio < 0.56f;
			#endif

			panelSafeArea.anchorMin = Vector2.zero;
			panelSafeArea.anchorMax = Vector2.one;
			panelSafeArea.offsetMin = Vector2.zero;
			panelSafeArea.offsetMax = Vector2.zero;

			if (isIphoneX) {
				Rect safeArea = Screen.safeArea;

				#if UNITY_EDITOR 
				// Safe area Iphone X: bottom = 34px, top = 44px
				safeArea = new Rect (0f, 34f / 812f * Screen.height, Screen.width, (812f - 34f - 44f) / 812f * Screen.height);
				#endif

				float height = panelSafeArea.rect.height;
				panelSafeArea.offsetMin = new Vector2 (0f, (safeArea.yMin / Screen.height) * height);
				panelSafeArea.offsetMax = new Vector2 (0f, (safeArea.yMax / Screen.height) * height - height);
			} 
		}
	}
}
