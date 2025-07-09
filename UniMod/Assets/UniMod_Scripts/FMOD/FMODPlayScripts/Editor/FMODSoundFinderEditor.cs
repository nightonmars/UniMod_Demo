using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FMODSoundFinder))]
public class FMODSoundFinderEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); // Draw the default Inspector

        FMODSoundFinder finder = (FMODSoundFinder)target;

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Search Functions", EditorStyles.boldLabel);

        // Search by FMODSound object
        if (GUILayout.Button("Search by FMODSound"))
        {
            finder.SearchBySound();
        }

        // Search by sound name
        finder.searchSoundName = EditorGUILayout.TextField("Search Sound Name", finder.searchSoundName);
        if (GUILayout.Button("Search by Name"))
        {
            finder.SearchBySoundName();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Utility Functions", EditorStyles.boldLabel);

        // Button to log all sounds in the scene
        if (GUILayout.Button("Log All Scene Sounds"))
        {
            finder.LogAllSceneSounds();
        }

        EditorGUILayout.Space();
        EditorGUILayout.LabelField("Search Results", EditorStyles.boldLabel);

        if (finder.searchResults != null && finder.searchResults.Count > 0)
        {
            foreach (var result in finder.searchResults)
            {
                if (result != null)
                {
                    EditorGUILayout.ObjectField("Found GameObject", result, typeof(GameObject), true);
                }
            }
        }
        else
        {
            EditorGUILayout.LabelField("No results found.");
        }
    }
}