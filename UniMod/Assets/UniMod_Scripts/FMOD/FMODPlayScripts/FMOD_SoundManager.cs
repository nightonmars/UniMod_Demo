using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class FMOD_SoundManager : MonoBehaviour
{
    public static FMOD_SoundManager Instance { get; private set; }

    [Header("Sound List")]
    public List<FMODSound> sounds; // Assign multiple ScriptableObjects in the Inspector

    [Header("Categorized Sound Lists")]
    [SerializeField]
    private List<CategoryGroup> categories = new List<CategoryGroup>();
    public List<CategoryGroup> Categories => categories; //getter for the CSV script

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public List<FMODSound> GetSoundsByCategory(string category)
    {
        var group = categories.Find(c => c.categoryName == category);
        return group?.sounds ?? new List<FMODSound>();
    }

    // Nested class for category groups
    [System.Serializable]
    public class CategoryGroup
    {
        public string categoryName;
        public List<FMODSound> sounds = new List<FMODSound>();
    }

#if UNITY_EDITOR
    // Editor-only function to organize sounds by category
    public void OrganizeSoundsByCategory()
    {
        categories.Clear();
        if (sounds == null) return;

        Dictionary<string, List<FMODSound>> categoryDict = new Dictionary<string, List<FMODSound>>();

        foreach (var sound in sounds)
        {
            if (sound == null || string.IsNullOrEmpty(sound.category)) continue;

            if (!categoryDict.ContainsKey(sound.category))
            {
                categoryDict[sound.category] = new List<FMODSound>();
            }

            categoryDict[sound.category].Add(sound);
        }

        foreach (var pair in categoryDict)
        {
            categories.Add(new CategoryGroup
            {
                categoryName = pair.Key,
                sounds = pair.Value
            });
        }

        EditorUtility.SetDirty(this); // Mark the object as dirty to save changes
    }
#endif
}
