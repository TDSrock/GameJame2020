using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Text;


public class AsyncSceneLoader : MonoBehaviour
{

    public Image loadingBar;
    public AsyncOperation nextSceneLoadOperation;
    public AsyncOperation previouseSceneUnloadOperation;
    public Scene previouseScene;
    public PersisntSceneManagementComponent sceneManagementComponent;
    public bool loadingAssets = false;
    private float dispProgress = 0f;
    public string loadingText = "";
    public Text loadingTextUIElement;


    // Use this for initialization
    void Start()
    {
        sceneManagementComponent = PersisntSceneManagementComponent.instance;
        StartAsyncLoadScene(sceneManagementComponent.pathOfScene);
    }

    void FixedUpdate()
    {
        var progress = GetProgressPercent();
        dispProgress = Mathf.Lerp(dispProgress, progress, .2f);
        loadingBar.fillAmount = dispProgress;
        loadingText = GetLoadingText(dispProgress);
        loadingTextUIElement.text = loadingText;
    }

    /// <summary>
    /// Start async loading of a scene, should be called only by the persistentSceneManagementObject!
    /// </summary>
    /// <param name="scenePath"></param>
    public void StartAsyncLoadScene(string scenePath)
    {
        nextSceneLoadOperation = SceneManager.LoadSceneAsync(scenePath);
    }

    public float GetProgressPercent()
    {
        if (nextSceneLoadOperation == null)
        {
            return 0;
        }
        return nextSceneLoadOperation.progress;
    }

    public string GetLoadingText(float progress)
    {
        var builder = new StringBuilder();
        builder.Append("Loading ");
        builder.AppendLine(sceneManagementComponent.pathOfScene);
        builder.Append("Progress: ");
        builder.AppendFormat("{0:0.##}", dispProgress * 100);
        return builder.ToString();
    }
}
