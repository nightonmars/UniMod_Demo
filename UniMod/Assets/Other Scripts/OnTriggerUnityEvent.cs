using System;
using UnityEngine;
using UnityEngine.Events;

public class OnTriggerUnityEvent : MonoBehaviour
{
   [SerializeField] private UnityEvent onTriggerEnter;
   [SerializeField] private UnityEvent onTriggerExit;
   [SerializeField] private string triggerObject;
   [SerializeField] private bool triggerOnEnter = true;
   [SerializeField] private bool triggerOnExit = false;
   private bool playOnce = false;
   

   private void OnTriggerEnter(Collider other)
   {
      if (other.CompareTag(triggerObject) && triggerOnEnter && playOnce == false)
      {
         onTriggerEnter.Invoke();
         playOnce = true;
      }
   }
   
   private void OnTriggerExit(Collider other)
   {
      if (other.CompareTag(triggerObject) && triggerOnExit && playOnce == true)
      {
         onTriggerExit.Invoke();
         playOnce = true;
      }
   }
}
