using UnityEngine;
using UnityEngine.Events;

public class StartBelt : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private UnityEvent startBelt;

    public void Belt()
    {
        animator.SetTrigger("StartBelt");
        startBelt.Invoke();
    }
    

 
}
