using System;
using System.Collections.Generic;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayFMODMultipleSounds : MonoBehaviour
{
    public string title = "Unnamed Audio Controller";
    [System.Serializable]
    public class SoundData
    {
        public FMODSound sound; // The FMOD sound
        public List<FMODParameter> parameters = new List<FMODParameter>(); // List of parameters for this sound
    }

    [System.Serializable]
    public class FMODParameter
    {
        public string name;
        public float value;
    }

    public List<SoundData> sounds = new List<SoundData>(); // List of sound data
    [SerializeField] public GameObject gameObjectPosition;
    private EventInstance eventInstance;
    public bool playOnStart = false;
    
    public float this[string parameterName, int index = 0]
    {
        get => GetParameterValue(index, parameterName);
        set => SetParameter(index, parameterName, value);
    }

    void Awake()
    {
        if (gameObjectPosition == null)
        {
            gameObjectPosition = gameObject; // Fallback to the attached GameObject. 
        }
    }
    void Start()
    {
        if (playOnStart && sounds.Count > 0)
        {
            PlaySoundAttached(0); // Play the first sound in the list as an example
        }
    }
    public void ApplyParameters(EventInstance instance, List<FMODParameter> parameters)
    {
        if (instance.isValid())
        {
            foreach (var param in parameters)
            {
                if (!string.IsNullOrEmpty(param.name))
                {
                    instance.setParameterByName(param.name, param.value);
                }
            }
        }
    }
    
    private Dictionary<int, EventInstance> activeEventInstances = new Dictionary<int, EventInstance>();
    public void PlaySound(int index)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return;
        }

        var soundData = sounds[index];
        if (soundData.sound == null)
        {
            Debug.LogWarning($"No FMODSound assigned at index {index}.");
            return;
        }

        // Create the event instance.
        EventInstance instance = RuntimeManager.CreateInstance(soundData.sound.GetEventReference());

        // Apply parameters and attributes.
        ApplyParameters(instance, soundData.parameters);
        if (soundData.sound.is3D)
        {
            instance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObjectPosition));
        }

        // Start the sound.
        instance.start();

        // Store the instance with the sound index.
        activeEventInstances[index] = instance;
    }

    public void PlaySoundAttached(int index)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return;
        }

        var soundData = sounds[index];
        if (soundData.sound == null)
        {
            Debug.LogWarning($"No FMODSound assigned at index {index}.");
            return;
        }

        // Create the event instance.
        EventInstance instance = RuntimeManager.CreateInstance(soundData.sound.GetEventReference());
    
        // Attach the instance to the specified GameObject.
        RuntimeManager.AttachInstanceToGameObject(instance, gameObjectPosition);

        // Apply parameters.
        ApplyParameters(instance, soundData.parameters);

        // Start the sound.
        instance.start();

        // Log the action.
        Debug.Log($"Playing attached sound: {soundData.sound.name}");

        // Store the instance.
        activeEventInstances[index] = instance;
    }

    public void PlayOneShot(int index)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return;
        }

        var soundData = sounds[index];
        if (soundData.sound == null)
        {
            Debug.LogWarning($"No FMODSound assigned at index {index}.");
            return;
        }

        RuntimeManager.PlayOneShot(soundData.sound.GetEventReference(), transform.position);
        Debug.Log($"Playing one-shot sound: {soundData.sound.name}");
    }
    public void PlayOneShotWithParam(int index)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return;
        }

        var soundData = sounds[index];
        if (soundData.sound == null)
        {
            Debug.LogWarning($"No FMODSound assigned at index {index}.");
            return;
        }

        EventInstance instance = RuntimeManager.CreateInstance(soundData.sound.GetEventReference());

        // Apply parameters directly
        foreach (var param in soundData.parameters)
        {
            if (!string.IsNullOrEmpty(param.name))
            {
                instance.setParameterByName(param.name, param.value);
            }
        }

        // Set 3D attributes based on this object's position
        instance.set3DAttributes(RuntimeUtils.To3DAttributes(transform.position));

        instance.start();
        instance.release();
    }


    public void StopSound(int index)
    {
        if (!activeEventInstances.TryGetValue(index, out EventInstance instance))
        {
            Debug.LogWarning("Sound instance not found for index: " + index);
            return;
        }

        instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        instance.release();
        activeEventInstances.Remove(index);
    }

    public void SetParameter(int index, string parameter, float value)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return;
        }

        // Update the local sound data parameters
        var soundData = sounds[index];
        bool parameterFound = false;
        foreach (var param in soundData.parameters)
        {
            if (param.name == parameter)
            {
                param.value = value;
                parameterFound = true;
                break;
            }
        }

        if (!parameterFound)
        {
            Debug.LogWarning($"Parameter '{parameter}' not found for sound at index {index}.");
            return;
        }

        // Now update the active event instance from the dictionary
        if (activeEventInstances.TryGetValue(index, out EventInstance instance))
        {
            if (instance.isValid())
            {
                instance.setParameterByName(parameter, value);
            }
            else
            {
                Debug.LogWarning($"The instance for sound index {index} is not valid.");
            }
        }
        else
        {
          //  Debug.LogWarning($"No active event instance found for sound at index {index}.");
        }
    }
 

    public void LogSoundInfo()
    {
        // Log the GameObject name
        Debug.Log($"GameObject: {gameObject.name}");

        if (sounds == null || sounds.Count == 0)
        {
            Debug.LogWarning("No sounds assigned.");
            return;
        }

        for (int i = 0; i < sounds.Count; i++)
        {
            var soundData = sounds[i];
            if (soundData.sound != null)
            {
                Debug.Log($"Sound {i}: {soundData.sound.name}");
                Debug.Log($"FMOD GUID Reference: {soundData.sound.GetEventReference()}");

                if (soundData.parameters.Count > 0)
                {
                    foreach (var param in soundData.parameters)
                    {
                        Debug.Log($"Parameter Name: {param.name}, Value: {param.value}");
                    }
                }
                else
                {
                    Debug.Log("No parameters set for this sound.");
                }
            }
            else
            {
                Debug.Log($"Sound {i}: No FMODSound assigned.");
            }
        }
    }
    
    public float GetParameterValue(int index, string parameterName)
    {
        if (index < 0 || index >= sounds.Count)
        {
            Debug.LogWarning("Invalid sound index.");
            return -1f; // Return an invalid value
        }

        var soundData = sounds[index];

        foreach (var param in soundData.parameters)
        {
            if (param.name == parameterName)
            {
                return param.value;
            }
        }

        Debug.LogWarning($"Parameter '{parameterName}' not found for sound at index {index}.");
        return -1f;
    }
}
