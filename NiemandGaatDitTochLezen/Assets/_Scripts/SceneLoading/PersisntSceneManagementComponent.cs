﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PersisntSceneManagementComponent : MonoBehaviour
{

    #region Singleton

    public static PersisntSceneManagementComponent instance;

    void Awake()
    {

        if (instance != null)
        {
            Debug.LogWarningFormat("More than one instance of PersisntSceneManagementComponent found! Second found on {0}", this.gameObject.name);
            Destroy(this.gameObject);
            return;
        }
        Debug.LogFormat("PersisntSceneManagementComponent found! found on {0}", this.gameObject.name);
        DontDestroyOnLoad(transform.gameObject);
        instance = this;
    }

    #endregion

    public AsyncSceneLoader sceneLoader;
    public string pathOfScene;
    public President presidentToFire;

    // Use this for initialization
    void Start()
    {

    }

    /// <summary>
    /// Loads a scene relative to the _Scenes folder in our project, The .unity extension is added in the method so DO NOT include it.
    /// </summary>
    /// <param name="NameOfSceneToLoad">Name of the scene relative too the _Scenes folder</param>
    public void LoadSceneWithLoadScreen(string NameOfSceneToLoad)
    {
        this.pathOfScene = "Assets/_Scenes/" + NameOfSceneToLoad + ".unity";
        SceneManager.LoadScene("Assets/_Scenes/LoadingScreenScene.unity");
    }

    [ContextMenu("TestLoad")]
    public void TestLoadScene()
    {
        LoadSceneWithLoadScreen("PlayingScene");
    }

}
