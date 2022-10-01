using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInteractionCanvasController : MonoBehaviour
{

    //Support singleton
    private static BlockInteractionCanvasController instance;

    public static BlockInteractionCanvasController Instance
    {


        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<BlockInteractionCanvasController>();
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }


    void Awake()
    {
        //		DebugLogging.Log("PopupNoticeController Awake");
        if (instance == null)
        {
            //If I am the first instance, make me the Singleton
            instance = this;
            gameObject.SetActive(false);
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            //If a Singleton already exists and you find
            //another reference in scene, destroy it!
            if (this != instance)
                Destroy(this.gameObject);
        }

    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
