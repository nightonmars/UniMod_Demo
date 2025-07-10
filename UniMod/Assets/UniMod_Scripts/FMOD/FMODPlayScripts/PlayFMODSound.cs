using FMOD.Studio;
using FMODUnity;
using UnityEngine;

public class PlayFMODSound : MonoBehaviour
{
    public int soundIndex; // Used to store the dropdown index
    public FMODSound sound; // The selected sound
    public GameObject gameObjectPosition;
    private EventInstance eventInstance;
    public bool playOnStart = false;
    
    
    [System.Serializable]
    public class FMODParameter
    {
        public string name;
        public float value;
    }

    public FMODParameter[] parameters; // Arrayed parameters for FMOD sound parameter

    void Awake()
    {
        if (gameObjectPosition == null)
        {
            gameObjectPosition = gameObject; // Fallback to the attached GameObject
        }
    }
    
    void Start()
    {
        if (playOnStart)
        {
            PlaySoundAttached();
        }
    }

    public void ApplyParameters(EventInstance instance)
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

    public void PlaySound()
    {
        if (sound == null)
        {
            Debug.LogWarning("No FMODSound assigned.");
            return;
        }

        // Initialize the event instance
        eventInstance = RuntimeManager.CreateInstance(sound.GetEventReference());

        // Apply parameters and attributes
        ApplyParameters(eventInstance);
        if (sound.is3D)
        {
            eventInstance.set3DAttributes(RuntimeUtils.To3DAttributes(gameObjectPosition));
        }

        // Play sound
            eventInstance.start();
            Debug.Log($"Playing sound: {sound.name}");
        
    }

    public void PlaySoundAttached()
    {
        if (sound == null)
        {
            Debug.LogWarning("No FMODSound assigned.");
            return;
        }

        eventInstance = RuntimeManager.CreateInstance(sound.GetEventReference());
        RuntimeManager.AttachInstanceToGameObject(eventInstance, gameObjectPosition);
        ApplyParameters(eventInstance);
        eventInstance.start();
        Debug.Log($"Playing attached sound: {sound.name}");
    }

    public void PlayOneShot()
    {
        if (sound == null)
        {
            Debug.LogWarning("No FMODSound assigned.");
            return;
        }
        RuntimeManager.PlayOneShot(sound.GetEventReference(), transform.position);
        Debug.Log($"Playing one-shot sound: {sound.name}");
    }
    
    public void PlayOneShotWithParam()
    {
        if (sound == null)
        {
            Debug.LogWarning("No FMODSound assigned.");
            return;
        }

        // Create instance
        FMOD.Studio.EventInstance instance = RuntimeManager.CreateInstance(sound.GetEventReference());

        // Apply parameters
        foreach (var param in parameters)
        {
            if (!string.IsNullOrEmpty(param.name))
            {
                instance.setParameterByName(param.name, param.value);
            }
        }

        // Set 3D attributes
        var attributes = gameObjectPosition != null
            ? RuntimeUtils.To3DAttributes(gameObjectPosition)
            : RuntimeUtils.To3DAttributes(transform);

        instance.set3DAttributes(attributes);

        // Play and release
        instance.start();
        instance.release();

        Debug.Log($"Playing one-shot sound with parameters: {sound.name}");
    }

    
    
    public void StopSound()
    {
        if (eventInstance.isValid())
        {
            eventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
            eventInstance.release();
            Debug.Log($"Stopped sound: {sound.name}");
        }
    }
    
    public void SetParameter(string parameter, float value)
    {
        if (eventInstance.isValid())
        {
            foreach (var param in parameters)
            {
                if (param.name == parameter)
                {
                    param.value = value; // Update the local parameter value
                    eventInstance.setParameterByName(parameter, value); // Update FMOD parameter
                    Debug.Log($"Updated parameter: {parameter} to {value}");
                    return; // Exit after finding and updating the matching parameter
                }
            }

            Debug.LogWarning($"Parameter '{parameter}' not found in the local parameters array.");
        }
        else
        {
            Debug.LogWarning("Event instance is not valid. Did you start it?");
        }
    }
    //this is used to collect sound info from the soundmanager
    public void LogSoundInfo()
    {
        // Log the GameObject name
        Debug.Log($"GameObject: {gameObject.name}");
    
        if (sound == null)
        {
            Debug.LogWarning("No FMODSound assigned.");
            return;
        }

        // Log the sound name
        Debug.Log($"Sound Name: {sound.name}");
        Debug.Log($"FMOD GUID Reference: {sound.GetEventReference()}");

        // Log the parameters if they exist
        if (parameters != null && parameters.Length > 0)
        {
            foreach (var param in parameters)
            {
                Debug.Log($"Parameter Name: {param.name}, Value: {param.value}");
            }
        }
        else
        {
            Debug.Log("No parameters set for this sound.");
        }
    }
    
    private void OnDestroy()
    {
        if (eventInstance.isValid())
        {
            eventInstance.release();
        }
    }

 
}
