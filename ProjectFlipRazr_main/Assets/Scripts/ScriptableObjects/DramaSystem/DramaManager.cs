using UnityEngine;
using System.Collections.Generic;
using static DramaSystem;

public class DramaManager : MonoBehaviour
{
    public DramaSystem dramaSystem; // Assign in Inspector
    private Dictionary<Quest, int> questProgressions = new Dictionary<Quest, int>();

    void Start()
    {
        // Initialize progression for each quest
        foreach (Quest quest in dramaSystem.quests)
        {
            questProgressions[quest] = 0; // Start each quest at progression 0
        }
    }

    void Update()
    {
        // Example: Trigger next progression stage for a specific quest when space key is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            foreach (var quest in dramaSystem.quests)
            {
                UpdateQuestProgression(quest);
            }
        }
    }

    private void UpdateQuestProgression(Quest quest)
    {
        if (questProgressions[quest] < quest.totalProgressionStages && questProgressions[quest] < quest.progress.Length)
        {
            HandleQuestProgression(quest.progress[questProgressions[quest]]);
            questProgressions[quest]++; // Increment progression for this quest

            if (questProgressions[quest] >= quest.totalProgressionStages)
            {
                quest.isCompleted = true;
            }
        }
    }

    private void HandleQuestProgression(Progression progression)
    {
        // Handle actions for the current progression stage
        for (int i = 0; i < progression.prefabObjects.Length; i++)
        {
            GameObject prefab = progression.prefabObjects[i];
            Vector3 spawnPosition = progression.spawnPositions[i];
            ActionOption action = progression.prefabActions[i];

            HandlePrefabAction(prefab, action, spawnPosition);
        }
    }

    void HandlePrefabAction(GameObject prefab, ActionOption action, Vector3 spawnPosition)
    {
        if (prefab == null) return;

        switch (action)
        {
            case ActionOption.Instantiate:
                Instantiate(prefab, spawnPosition, Quaternion.identity);
                break;
            //case ActionOption.Activate:
            //    prefab.SetActive(true);
            //    break;
            //case ActionOption.Deactivate:
            //    prefab.SetActive(false);
            //    break;
            //case ActionOption.Destroy:
            //    Destroy(prefab);
            //    break;
            case ActionOption.None:
            default:
                // No action
                break;
        }
    }
}
