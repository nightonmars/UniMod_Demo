#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PlayFMODMultipleSounds))]
public class PlayFMODMultipleSoundsEditor : Editor
{
    public override void OnInspectorGUI()
    {
        
        PlayFMODMultipleSounds playFMODMultipleSounds = (PlayFMODMultipleSounds)target;
        FMOD_SoundManager soundManager = FindObjectOfType<FMOD_SoundManager>();
        

        // Line style for dividers
        GUIStyle lineStyle = new GUIStyle();
        lineStyle.normal.background = EditorGUIUtility.whiteTexture; // Base texture
        lineStyle.margin = new RectOffset(0, 0, 4, 4);               // Spacing around the line
        lineStyle.fixedHeight = 1;                                   // Line thickness

        Color originalColor = GUI.color;
        GUI.color = Color.white;                                      // Line color (light gray)
        
        EditorGUILayout.LabelField("Component Title", EditorStyles.boldLabel);
        playFMODMultipleSounds.title = EditorGUILayout.TextField(playFMODMultipleSounds.title);
        GUILayout.Space(10);
        GUILayout.Box("", lineStyle, GUILayout.ExpandWidth(true));
        GUILayout.Space(10);

        if (soundManager != null)
        {
            var availableSounds = soundManager.sounds;

            if (availableSounds != null && availableSounds.Count > 0)
            {
                EditorGUILayout.LabelField("Sounds", EditorStyles.boldLabel);
                GUILayout.Space(5);

                for (int i = 0; i < playFMODMultipleSounds.sounds.Count; i++)
                {
                    var soundData = playFMODMultipleSounds.sounds[i];
                    EditorGUILayout.BeginVertical("box");

                    int selectedIndex = availableSounds.IndexOf(soundData.sound);
                    string[] soundNames = availableSounds.ConvertAll(sound => string.IsNullOrEmpty(sound.category) ? sound.name : $"{sound.category}/{sound.name}").ToArray();

                    selectedIndex = EditorGUILayout.Popup($"Sound {i + 1}", selectedIndex, soundNames);

                    if (selectedIndex >= 0 && selectedIndex < availableSounds.Count)
                    {
                        soundData.sound = availableSounds[selectedIndex];
                    }

                    // Parameters Section
                    EditorGUILayout.LabelField("Parameters", EditorStyles.boldLabel, GUILayout.MaxWidth(100));
                    GUILayout.Space(5);

                    if (soundData.parameters != null)
                    {
                        for (int j = 0; j < soundData.parameters.Count; j++)
                        {
                            var param = soundData.parameters[j];

                            // Divider between parameters
                            if (j > 0)
                            {
                                GUILayout.Space(5);
                                GUILayout.Box("", lineStyle, GUILayout.ExpandWidth(true));
                                GUILayout.Space(5);
                            }

                            EditorGUILayout.BeginHorizontal();

                            EditorGUILayout.LabelField("Name", GUILayout.Width(40));
                            param.name = EditorGUILayout.TextField(param.name, GUILayout.MinWidth(100), GUILayout.ExpandWidth(false));

                            EditorGUILayout.LabelField("Value", GUILayout.Width(40));
                            param.value = EditorGUILayout.FloatField(param.value, GUILayout.Width(50));

                            // Remove Parameter Button
                            if (GUILayout.Button("X", GUILayout.Width(25)))
                            {
                                soundData.parameters.RemoveAt(j);
                                break;
                            }

                            EditorGUILayout.EndHorizontal();
                            GUILayout.Space(8); // Space between parameter entries
                        }
                    }


                    // Add Parameter Button
                    if (GUILayout.Button("Add Parameter", GUILayout.Width(120)))
                    {
                        soundData.parameters.Add(new PlayFMODMultipleSounds.FMODParameter());
                    }

                    // Remove Sound Button
                    if (GUILayout.Button("Remove Sound", GUILayout.Width(120)))
                    {
                        playFMODMultipleSounds.sounds.RemoveAt(i);
                        break;
                    }

                    EditorGUILayout.EndVertical();

                    // Divider between each Sound section
                    GUILayout.Space(10);
                    GUILayout.Box("", lineStyle, GUILayout.ExpandWidth(true));
                    GUILayout.Space(10);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("No sounds found in FMOD_SoundManager. Please assign sounds in the SoundManager.", MessageType.Warning);
            }
        }
        
        // Other properties
        playFMODMultipleSounds.gameObjectPosition = (GameObject)EditorGUILayout.ObjectField("3D position if other than gameobject", playFMODMultipleSounds.gameObjectPosition, typeof(GameObject), true);

        // Divider before Add Sound button
        GUILayout.Space(10);
        GUILayout.Box("", lineStyle, GUILayout.ExpandWidth(true));
        GUILayout.Space(5);

        // Add Sound Button
        if (GUILayout.Button("Add Sound"))
        {
            playFMODMultipleSounds.sounds.Add(new PlayFMODMultipleSounds.SoundData());
        }

        GUI.color = originalColor; // Reset to default color

        if (GUI.changed)
        {
            EditorUtility.SetDirty(playFMODMultipleSounds);
        }
    }
}
#endif
