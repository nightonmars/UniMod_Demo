using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayFMODSound))]
public class PlayFMODSoundEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Get the target object
        PlayFMODSound playFMODSound = (PlayFMODSound)target;

        // Try to find an FMOD_SoundManager in the scene
        FMOD_SoundManager soundManager = FindObjectOfType<FMOD_SoundManager>();

        if (soundManager != null)
        {
            var sounds = soundManager.sounds;

            if (sounds != null && sounds.Count > 0)
            {
                string[] soundNames = new string[sounds.Count];
                for (int i = 0; i < sounds.Count; i++)
                {
                    soundNames[i] = string.IsNullOrEmpty(sounds[i]?.category)
                        ? sounds[i]?.name ?? "Unnamed Sound"
                        : $"{sounds[i].category}/{sounds[i].name}";
                }

                playFMODSound.soundIndex = EditorGUILayout.Popup("Sound", playFMODSound.soundIndex, soundNames);

                if (playFMODSound.soundIndex >= 0 && playFMODSound.soundIndex < sounds.Count)
                {
                    playFMODSound.sound = sounds[playFMODSound.soundIndex];
                }
                
                if (playFMODSound.soundIndex >= sounds.Count)
                {
                    playFMODSound.soundIndex = 0; // Reset to a valid index
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No sounds found in FMOD_SoundManager. Please assign sounds in the SoundManager.", MessageType.Warning);
            }
        }
        else
        {
            EditorGUILayout.HelpBox("FMOD_SoundManager not found in the scene. Please add one to access sounds.", MessageType.Error);
        }

        // Draw parameters
        EditorGUILayout.LabelField("FMOD Parameters - set initial values", EditorStyles.boldLabel);
        if (playFMODSound.parameters != null)
        {
            for (int i = 0; i < playFMODSound.parameters.Length; i++)
            {
                var param = playFMODSound.parameters[i];
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Name", GUILayout.Width(50)); // Label for "Name"
                param.name = EditorGUILayout.TextField(param.name, GUILayout.ExpandWidth(true)); // Wider TextField

                EditorGUILayout.LabelField("Value", GUILayout.Width(50)); // Label for "Value"
                param.value = EditorGUILayout.FloatField(param.value, GUILayout.Width(100)); // FloatField with a fixed width
                EditorGUILayout.EndHorizontal();

            }
        }

        // Add parameter button
        if (GUILayout.Button("Add Parameter"))
        {
            ArrayUtility.Add(ref playFMODSound.parameters, new PlayFMODSound.FMODParameter());
        }

        // Remove parameter button
        if (GUILayout.Button("Remove Parameter"))
        {
            if (playFMODSound.parameters.Length > 0)
            {
                ArrayUtility.RemoveAt(ref playFMODSound.parameters, playFMODSound.parameters.Length - 1);
            }
        }

        // Other properties
        playFMODSound.gameObjectPosition = (GameObject)EditorGUILayout.ObjectField("GameObject", playFMODSound.gameObjectPosition, typeof(GameObject), true);
        playFMODSound.playOnStart = EditorGUILayout.Toggle("Play On Start", playFMODSound.playOnStart);

        if (GUI.changed)
        {
            EditorUtility.SetDirty(playFMODSound);
        }
    }
}
