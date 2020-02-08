using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.Extensions;
using System.Text;

public class GameManager : MonoBehaviour
{
    public int presidentNotesDuplicates = 2;
    public int fillerDuplicates = 2;
    public List<President> presidents = new List<President>();
    public List<ImageObject> filler = new List<ImageObject>();
    public GameObject rootGameobjectForFinding;
    public List<LootableDocumentHolder> lootables;
    // Start is called before the first frame update
    void Start()
    {
        SetupWorld();
    }
    [ContextMenu("Setup world debug")]
    void SetupWorld()
    {
        FindAllLootObjects();
        //decide who is corrupt
        var copy = new List<President>(presidents);
        foreach (var pres in presidents)
        {
            pres.isClean = false;
        }
        Random.InitState(System.DateTime.Now.Millisecond);
        presidents.RandomItem().isClean = true;
        
        for (int i = 0; i < presidentNotesDuplicates; i++)
        {
            foreach (President pres in presidents)
            {
                if (pres.isClean)
                {
                    foreach (var doc in pres.whenCleanNotes)
                    {
                        LootableDocumentHolder place = lootables.RemoveRandom();
                        place.document = doc;
                    }
                }
                else
                {
                    foreach (var doc in pres.whenCorruptNotes)
                    {
                        LootableDocumentHolder place = lootables.RemoveRandom();
                        place.document = doc;
                    }
                }
            }
        }
        for (int i = 0; i < fillerDuplicates; i++)
        {
            foreach (var doc in filler)
            {
                LootableDocumentHolder place = lootables.RemoveRandom();
                place.document = doc;
            }
        }
    }

    [ContextMenu("Find all lootie shooties")]
    void FindAllLootObjects()
    {
        
        if (rootGameobjectForFinding)
        {
            lootables = new List<LootableDocumentHolder>( rootGameobjectForFinding.GetComponentsInChildren<LootableDocumentHolder>());
        }
        foreach(LootableDocumentHolder loot in lootables)
        {
            loot.document = null;
        }
    }

    [ContextMenu("Setup world president bug check")]
    void DebugThing()
    {
        int attempts = 200000;

        Dictionary<President, int> presidentsPicked = new Dictionary<President, int>();
        foreach (President pres in presidents)
        {
            presidentsPicked.Add(pres, 0);
        }
        for (int i = 0; i < attempts; i++)
        {
            SetupWorld();
            foreach (President pres in presidents)
            {
                if (pres.isClean)
                {
                    presidentsPicked[pres]++;
                }
            }
        }
        StringBuilder str = new StringBuilder();
        int count = 0;
        foreach (var pres in presidentsPicked)
        {
            str.AppendFormat("{0} was picked as the clean president {1} times\n", pres.Key.name, pres.Value);
            count += pres.Value;
        }
        Debug.Log(str);
        if (count != attempts)
        {
            Debug.LogWarning("Something went wrong!");
        }

    }
}
