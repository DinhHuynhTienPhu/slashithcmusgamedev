using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;

public class PressBackKeyController : MonoBehaviour {
	public bool enableCheckBackKey = true;

	List<Action> onKeyBackClicked = new List<Action>();
	bool canPressButtonEscape = true; // check xem có bấm nút back hay chưa

	static PressBackKeyController ins;
	public static PressBackKeyController instance{
		get{
			return ins;
		}
	}

	void Awake(){
		if(ins == null){
			//If I am the first instance, make me the Singleton
			ins = this;
			DontDestroyOnLoad(this.gameObject);
		}else{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != ins)
				Destroy(this.gameObject);
		}
	}

	void Start(){
		#if UNITY_ANDROID
		StartCoroutine(DoActionCheckPressBackKey());
		#endif
	}

	WaitForSecondsRealtime waitForSecondsRealtime = new WaitForSecondsRealtime(0.2f);
	IEnumerator DoActionCheckPressBackKey() {
		while(true){
			yield return null;
			if (canPressButtonEscape){
				if(Input.GetKeyUp(KeyCode.Escape)) {
					if( enableCheckBackKey
						&& onKeyBackClicked.Count > 0 
						&& !LoadingCanvas.Instance.IsShowing ()
					){
						canPressButtonEscape = false;
						try{
							onKeyBackClicked[onKeyBackClicked.Count - 1].Invoke();
						}catch(Exception e) {
							Debug.LogError(SceneManager.GetActiveScene().name + ": " + e.StackTrace);
							RemoveCurrentCallback(onKeyBackClicked[onKeyBackClicked.Count - 1]);
						}
						yield return waitForSecondsRealtime;
						canPressButtonEscape = true;
					}
				}
			}
		}
	}
		

	public void RegisterNewCallback(Action _onKeyBackClicked){
		#if UNITY_ANDROID
		onKeyBackClicked.Add(_onKeyBackClicked);
		#endif
	}

	public void RemoveCurrentCallback(Action callBack){
		#if UNITY_ANDROID
		onKeyBackClicked.Remove (callBack);
		#endif
	}

	public void ClearAllCallback(){
		#if UNITY_ANDROID
		onKeyBackClicked.Clear();
		#endif
	}
}
