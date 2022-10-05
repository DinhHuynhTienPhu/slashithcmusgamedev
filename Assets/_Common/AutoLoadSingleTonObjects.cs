using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;


public class AutoLoadSingleTonObjects : MonoBehaviour
{
	[Header("List Singleton: ")]
	public List<GameObject> singletonObjects;

	static AutoLoadSingleTonObjects ins;
	void Awake ()
	{
//		DebugLogging.Log("AlertPanel Awake");
		if(ins == null)
		{
			//If I am the first instance, make me the Singleton
			ins = this;
			DontDestroyOnLoad(this.gameObject);

			for (int i = 0; i < singletonObjects.Count;i++) {
				if(singletonObjects[i] != null
				){
					Instantiate(singletonObjects[i]);
				}
			}
		}
		else
		{
			//If a Singleton already exists and you find
			//another reference in scene, destroy it!
//			if(this != ins)
//				Destroy(this.gameObject);
		}

	}
}
