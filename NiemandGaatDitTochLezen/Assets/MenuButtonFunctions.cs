using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuButtonFunctions : MonoBehaviour
{
    public string gameSceneName;
    public void OnLoadMainGame()
    {
        PersisntSceneManagementComponent.instance.LoadSceneWithLoadScreen(gameSceneName);
    }
}
