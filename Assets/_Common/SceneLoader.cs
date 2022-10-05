using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;
//using AssetBundles;

//====================================================================================================
/// <summary>
/// Scene loader.
/// </summary>
//====================================================================================================
public class SceneLoader : MonoBehaviour
{
	public const string SceneGamePlay = "GamePlay";
	public const string SceneMainMenu = "MainMenu";
	
    public static Action OnSceneLoaded;
	public Animator anim;

	#region Static Variables

	public static bool isLoading{
		get{
            if (instance == null)
                return false;

			if (instance.gameObject.activeSelf){
				return true;
			}
			return false;
		}
	}

	#endregion
	//public Animator anim;
    public Text txtTips;
    public List<string> tips = new List<string> ();

    //----------------------------------------------------------------------------------------------------
    /// <summary>
    /// Awake this instance.
    /// </summary>
    //----------------------------------------------------------------------------------------------------


    public void Awake ()
	{
		if (instance != null && instance != this) {
			Destroy (gameObject);
			return;
		}
		instance = this;
		GameObject.DontDestroyOnLoad (instance.gameObject);
		instance.gameObject.SetActive (false);
	}

	private static SceneLoader Instance {
		get {
			if (instance == null) {
				instance = FindObjectOfType<SceneLoader> ();
				GameObject.DontDestroyOnLoad (instance.gameObject);
			}
			return instance;
		}
	}
	private static SceneLoader instance = null;

	public static void LoadLevel (string name, bool showLoader = true)
	{
		if (showLoader)
		{
			AudioController.StopMusic();
			Instance.gameObject.SetActive(true);
			Instance.StartCoroutine(LoadLevelRoutine(name));
		}
		else
		{
			SceneManager.LoadScene(name);
		}
	}


	private static IEnumerator LoadLevelRoutine(string name)
	{
		PressBackKeyController.instance.ClearAllCallback();

		Instance.gameObject.SetActive (true);
        yield return null;
		Instance.anim.SetTrigger ("FadeIn");
		yield return new WaitForSeconds(0.2f);

		var asyncLoad = SceneManager.LoadSceneAsync (name, LoadSceneMode.Single);
		yield return new WaitUntil (() => asyncLoad.isDone);

		// --- Fade out ---
		yield return null;
		Instance.anim.SetTrigger ("FadeOut");
		yield return new WaitForSeconds(0.2f);
        yield return null;
        if (OnSceneLoaded != null) {
            OnSceneLoaded();
        }

		Instance.gameObject.SetActive (false);
	}


}
