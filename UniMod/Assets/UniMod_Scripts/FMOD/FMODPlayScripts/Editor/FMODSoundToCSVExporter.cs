using UnityEngine;
using UnityEditor;
using System.IO;
using System.Text;

public class FMODSoundToCSVExporter : MonoBehaviour
{
    [MenuItem("Tools/Export FMOD Sounds to CSV")]
    public static void ExportToCSV()
    {
        var soundManager = GameObject.FindObjectOfType<FMOD_SoundManager>();
        if (soundManager == null)
        {
            Debug.LogError("FMOD_SoundManager not found in scene.");
            return;
        }

        StringBuilder csv = new StringBuilder();
        csv.AppendLine("Category,Sound Name,3D Sound,Event Path,Comment");

        foreach (var categoryGroup in soundManager.Categories) // Assumes public getter
        {
            foreach (var sound in categoryGroup.sounds)
            {
                if (sound == null) continue;

                string category = categoryGroup.categoryName;
                string soundName = sound.name;
                string is3D = sound.is3D ? "Yes" : "No";
                string eventPath = sound.GetEventReference().IsNull ? "NULL" : sound.GetEventReference().Path;
                string comment = EscapeForCSV(sound.comment);

                csv.AppendLine($"{category},{soundName},{is3D},{eventPath},{comment}");
            }
        }

        string path = EditorUtility.SaveFilePanel("Save FMOD Sounds CSV", "", "FMOD_Sounds.csv", "csv");
        if (!string.IsNullOrEmpty(path))
        {
            File.WriteAllText(path, csv.ToString());
            Debug.Log($"FMOD sounds exported to CSV:\n{path}");
        }
    }

// Ensures commas, quotes, or newlines in comments don't break the CSV
    private static string EscapeForCSV(string value)
    {
        if (string.IsNullOrEmpty(value)) return "";
        value = value.Replace("\"", "\"\""); // Escape quotes
        if (value.Contains(",") || value.Contains("\n"))
            return $"\"{value}\""; // Enclose in quotes if needed
        return value;
    }

}
