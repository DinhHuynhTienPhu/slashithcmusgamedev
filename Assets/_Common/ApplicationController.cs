using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using Facebook.Unity;
using System;
using System.Threading.Tasks;

public class ApplicationController : MonoBehaviour
{
	public DataManager dataManager;
	public static bool enableSave = true;
	
	public static ApplicationController Instance {
		get {
			if (_instance == null) {
				_instance = FindObjectOfType<ApplicationController> ();
				_instance.InitData();
				DontDestroyOnLoad (_instance.gameObject);
			}
			return _instance;
		}
	}
	static ApplicationController _instance;

	void Awake ()
	{
		if (_instance == null) {
            //ClearSave();
			_instance = this;
			_instance.InitData();
			DontDestroyOnLoad (_instance.gameObject);
		} else if (this != _instance) {
			Destroy (gameObject);
		}
	}


	void InitData()
	{
		dataManager = DataManager.Instance;
	}

	void Start ()
	{
        //		Tapdaq.AdManager.LoadInterstitial ();
        //		Tapdaq.AdManager.LoadRewardedVideo ();
        //		FB.Init(this.OnInitComplete, this.OnHideUnity);
        //		Debug.Log("FB.Init() called with " + FB.AppId);

        
#if UNITY_ANDROID
	    StartCoroutine(DoActionCheckPressBackKey());
#endif

	}

   

    



	private void OnApplicationPause(bool pauseStatus)
	{
		if (pauseStatus && enableSave)
		{
			DataManager.Save();
		}
	}

	#if UNITY_EDITOR
	private void OnApplicationQuit()
	{
		if (enableSave)
		{
            Debug.Log("saved");
			DataManager.Save();
		}
	}
#endif
	[ContextMenu ("Clear")]
	public void ClearSave ()
	{
		PlayerPrefs.DeleteAll();
		DataManager.ResetToDefault();
	}



    #region Back Button
    bool canPressButtonEscape = true;
//    void Update() {
//		if (Input.GetKeyDown(KeyCode.Escape)  && !DialogController.instance.IsDialogShowing()) {
//            if (canPressButtonEscape) {
//                canPressButtonEscape = false;
//               
//            }
//        }
//    }

    List<Action> onKeyBackClicked = new List<Action>();
    IEnumerator DoActionCheckPressBackKey() {
        while(true){
            yield return null;
            if (canPressButtonEscape){
                if(Input.GetKeyUp(KeyCode.Escape)) {
                    if(onKeyBackClicked.Count > 0 
//                        && !LoadingCanvas.Instance.IsShowing ()
                    ){
                        canPressButtonEscape = false;
                        try{
                            onKeyBackClicked[onKeyBackClicked.Count - 1].Invoke();
                        }catch(Exception e) {
//                            Debug.LogError(SceneManager.GetActiveScene().name + ": " + e.StackTrace);
                            RemoveBackKeyCallback(onKeyBackClicked[onKeyBackClicked.Count - 1]);
                        }
                        yield return new WaitForSecondsRealtime(0.2f);
                        canPressButtonEscape = true;
                    }
                }
            }
        }
    }
		

    public void RegisterBackKeyCallback(Action _onKeyBackClicked){
#if UNITY_ANDROID
        onKeyBackClicked.Add(_onKeyBackClicked);
#endif
    }

    public void RemoveBackKeyCallback(Action callBack){
#if UNITY_ANDROID
        onKeyBackClicked.Remove (callBack);
#endif
    }

    public void ClearAllCallback(){
#if UNITY_ANDROID
        onKeyBackClicked.Clear();
#endif
    }



    #endregion

}

public static class MonoBehaviourExtensions {
    public static void Invoke(this MonoBehaviour me, Action theDelegate, float time) {
        me.StartCoroutine(ExecuteAfterTime(theDelegate, time));
    }

    private static IEnumerator ExecuteAfterTime(Action theDelegate, float delay) {
        yield return new WaitForSeconds(delay);
        theDelegate();
    }
}
