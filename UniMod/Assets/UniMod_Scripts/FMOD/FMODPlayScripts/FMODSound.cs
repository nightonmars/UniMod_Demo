using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(fileName = "NewSound", menuName = "Audio/FMOD Sound")]
public class FMODSound : ScriptableObject
{
    [Header("Categorization")]
    public string category;

    [Header("FMOD Event")]
    [SerializeField]
    private EventReference eventReference;
    
    [Header("Settings")]
    public bool is3D = true;
    
    [Header("Comment")]
    [TextArea(3, 10)] // minLines = 3, maxLines = 10
    public string comment;

    public EventReference GetEventReference() => eventReference;
}