using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagement : MonoBehaviour
{
    static GameManagement instance;
    public static GameManagement Instance
    {
        get {
            if(instance == null)
            {
                Debug.LogError("Some bitch forgot to actually put in the GameManagement object.");
            }
            return instance; }
    }

    [Header("Document related shit")]
    GameObject NoRelevantItemsFoundUI;

    void Awake()
    {
        instance = this;
    }

    public void OpenDocuemnt(Document doc)
    {
        //fuck up the state a bit

        //do doc logic
        if(doc == null)
        {
            NoRelevantItemsFoundUI.SetActive(true);
        }
        //now the fun shit comes
    }

    public void CloseDocument()
    {
        //restore state
    }
}
