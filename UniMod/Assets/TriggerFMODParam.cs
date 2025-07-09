using UnityEngine;

public class TriggerFMODParam : MonoBehaviour
{
    [SerializeField] private PlayFMODSound playFMODSound;
    public void SetFmodParamStart()
    {
        playFMODSound.SetParameter("BeltFilter", 0);
    }
    
    public void SetFmodParamEnd()
    {
        playFMODSound.SetParameter("BeltFilter", 1);
    }
}
