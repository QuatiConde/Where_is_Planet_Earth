using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(BoxCollider))]
public class FeedbackInteraction : MonoBehaviour
{
    //References
    private GameObject feedback;

    //Events
    public UnityEvent OnInteract;
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    private void Start()
    {
        feedback = transform.Find("InteractionFeedback").gameObject;
        //Initial values
        feedback.SetActive(false);
    }

    public void Toggle(bool value)
    {
        if (value)
        {
            enabled = true;
            feedback.SetActive(false);
        }
        else
        {
            enabled = false;
            feedback.SetActive(true);
            PlayerInteraction.OnInteract.RemoveListener(Interact);
        }
    }

    private void Interact()
    {
        OnInteract?.Invoke();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnEnter?.Invoke();

            feedback.SetActive(true);

            PlayerInteraction.OnInteract.AddListener(Interact);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnExit?.Invoke();

            feedback.SetActive(false);

            PlayerInteraction.OnInteract.RemoveListener(Interact);
        }
    }
}