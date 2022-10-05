using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

using Random = UnityEngine.Random;
using UnityEditor;

public class GameInformation : MonoBehaviour
{

    public static GameInformation Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<GameInformation>();
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }
    static GameInformation _instance;

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(_instance.gameObject);
        }
        else if (this != _instance)
        {
            Destroy(gameObject);
        }
    }
    public List<List<GameObject>> stages = new List<List<GameObject>>();

    private void Start()
    {
    }


}