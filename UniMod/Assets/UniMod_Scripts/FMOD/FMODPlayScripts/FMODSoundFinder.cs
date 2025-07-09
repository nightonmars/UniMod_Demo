using UnityEngine;
using System.Collections.Generic;

public class FMODSoundFinder : MonoBehaviour
{
    [Header("Search Settings")]
    public FMODSound searchSound; // Reference to the FMODSound to search for
    public string searchSoundName; // Alternatively, search by name

    [HideInInspector]
    public List<GameObject> searchResults = new List<GameObject>(); // Stores the results

    /// <summary>
    /// Searches for all GameObjects in the scene using the specified FMODSound.
    /// </summary>
    public void SearchBySound()
    {
        if (searchSound == null)
        {
            Debug.LogWarning("FMODSoundFinder: Search sound is null.");
            return;
        }

        searchResults.Clear();

        // Find all PlayFMODSound components
        var soundComponents = FindObjectsByType<PlayFMODSound>(FindObjectsSortMode.None);
        
        foreach (var component in soundComponents)
        {
            if (component.sound == searchSound)
            {
                Debug.Log($"Found GameObject '{component.gameObject.name}' using sound '{searchSound.name}'.");
                searchResults.Add(component.gameObject);
            }
        }

        if (searchResults.Count == 0)
        {
            Debug.LogWarning($"FMODSoundFinder: No GameObjects found using sound '{searchSound.name}'.");
        }
    }

    /// <summary>
    /// Searches for all GameObjects in the scene using an FMODSound with the specified name.
    /// </summary>
    public void SearchBySoundName()
    {
        if (string.IsNullOrEmpty(searchSoundName))
        {
            Debug.LogWarning("FMODSoundFinder: Search sound name is null or empty.");
            return;
        }

        searchResults.Clear();

        // Find all PlayFMODSound components //THIS MIGHT BECOME A BUG - in taht case change sort mode
        var soundComponents = FindObjectsByType<PlayFMODSound>(FindObjectsSortMode.None);

        foreach (var component in soundComponents)
        {
            if (component.sound != null && component.sound.name == searchSoundName)
            {
                Debug.Log($"Found GameObject '{component.gameObject.name}' using sound '{searchSoundName}'.");
                searchResults.Add(component.gameObject);
            }
        }

        if (searchResults.Count == 0)
        {
            Debug.LogWarning($"FMODSoundFinder: No GameObjects found using sound '{searchSoundName}'.");
        }
    }
    
    // Use to pull all sounds
    [ContextMenu("pull sound")]
    public void LogAllSceneSounds()
    {
        // Find all PlayFMODSound components in the scene
        var soundComponents = FindObjectsByType<PlayFMODSound>(FindObjectsSortMode.None);

        foreach (var component in soundComponents)
        {
            if (component != null)
            {
                Debug.Log($"GameObject: {component.gameObject.name}");
                component.LogSoundInfo();
            }
        }
    }
}
