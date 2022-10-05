using UnityEngine;
using System.Collections;

public class LoadingCanvas : MonoBehaviour
{

	//Support singleton
	private static LoadingCanvas _instance;
	
	public static LoadingCanvas Instance {
		
		
		get {
			if (_instance == null) {
				_instance = GameObject.FindObjectOfType<LoadingCanvas> ();

				if (_instance == null) {
					Debug.LogWarning ("LoadingCanvas not found");
					return null;
				}
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad (_instance.gameObject);
			}
			
			return _instance;
		}
		set {
			_instance = value;
		}
	}
	
	
	void Awake() 
	{
		//		DebugLogging.Log("AlertPanel Awake");
		if(_instance == null)
		{
			//If I am the first instance, make me the Singleton
			_instance = this;
			gameObject.SetActive (false);
			DontDestroyOnLoad(this.gameObject);
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
			if(this != _instance)
				Destroy(this.gameObject);
		}

	}

	public void Show ()
	{
		//TODO:animating
		gameObject.SetActive (true);
		#if UNITY_ANDROID || UNITY_IOS
		if (Application.isMobilePlatform)
			Handheld.StartActivityIndicator();
		#endif
	}

	public void Hide ()
	{
		//TODO:animating
		gameObject.SetActive (false);
#if UNITY_ANDROID || UNITY_IOS
		if (Application.isMobilePlatform)
			Handheld.StopActivityIndicator();
	#endif
	}

	public bool IsShowing ()
	{
		return gameObject.activeInHierarchy;
	}
}
