using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FMOD_SoundManager))]
public class FMOD_SoundManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        FMOD_SoundManager soundManager = (FMOD_SoundManager)target;

        if (GUILayout.Button("Organize Sounds by Category"))
        {
            soundManager.OrganizeSoundsByCategory();
            Debug.Log("FMOD Sounds organized by category.");
        }
    }
}