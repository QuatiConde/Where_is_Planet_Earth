using UnityEngine;
using UnityEngine.Events;
using NaughtyAttributes;

public class DialogueTrigger : MonoBehaviour
{
    [Header("Settings")]
    public int dialogueIndex;
    public bool disableOnPlay = true;
    [Header("Should this trigger enable other object?")]
    public bool enableOther;
    [ShowIf("enableOther")] public GameObject enableObj;

    [Header("Events")]
    public UnityEvent OnShow; //Called when showing the dialogue

    public virtual void OnPlayerEnter()
    {
        //This method can be overrided
        ShowDialogue();
    }

    public virtual void OnPlayerExit()
    {
        //This method can be overrided
    }

    public virtual void ShowDialogue()
    {
        //This method can be overrided
        OnShow?.Invoke();

        DialogueManager.Instance.ShowDialogue(dialogueIndex);

        if (enableOther)
            enableObj.SetActive(true);

        if (disableOnPlay)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            OnPlayerExit();
        }
    }
}