using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SjorsGielen.Extensions;

public class GameManager : MonoBehaviour
{
    public int presidentNotesDuplicates = 2;
    public int fillerDuplicates = 2;
    public int numbOfCorruptFuckers = 4;
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
            pres.isClean = true;
        }
        for (int i = 0; i < numbOfCorruptFuckers; i++)
        {
            copy.RemoveRandom().isClean = false;
        }
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
}
