using UnityEngine;

public class DialogueInput : DialogueTrigger
{
    private GameObject feedback;

    private void Start()
    {
        //Get references
        feedback = transform.Find("InteractionFeedback").gameObject;
        //Initial values
        feedback.SetActive(false);
    }

    public override void OnPlayerEnter()
    {
        feedback.SetActive(true);

        PlayerInteraction.OnInteract.AddListener(ShowDialogue);
    }

    public override void OnPlayerExit()
    {
        feedback.SetActive(false);

        PlayerInteraction.OnInteract.RemoveListener(ShowDialogue);        
    }

    public override void ShowDialogue()
    {
        base.ShowDialogue();

        feedback.SetActive(false);

        PlayerInteraction.OnInteract.RemoveListener(ShowDialogue);
    }
}