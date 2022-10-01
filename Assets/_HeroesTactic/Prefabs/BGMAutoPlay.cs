using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class BGMAutoPlay : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
      
            AudioController.Play("Pretty");

        
    }
    private void OnEnable()
    {
         AudioController.PlayMusicPlaylist();
        AudioController.Play("Pretty");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {

         if(scene.name=="MainMenu")
        {

                AudioController.Play("Pretty");
                AudioController.ClearPlaylist();
            
        }
    }
}
