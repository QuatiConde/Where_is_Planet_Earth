public class DialogueInput : DialogueTrigger, IFeedback
{
    private FeedbackInteraction interaction;

    private void Start()
    {
        //Get references
        interaction = GetComponent<FeedbackInteraction>();
        //Initial values
        SubscribeFeedback();
    }

    public void SubscribeFeedback()
    {
        interaction.OnInteract.AddListener(OnInteract);
        interaction.OnEnter.AddListener(OnPlayerEnter);
        interaction.OnExit.AddListener(OnPlayerExit);
    }

    public override void OnPlayerEnter()
    {
        PlayerInteraction.OnInteract.AddListener(ShowDialogue);
    }

    public override void OnPlayerExit()
    {
        PlayerInteraction.OnInteract.RemoveListener(ShowDialogue);        
    }

    public void OnInteract()
    {
        ShowDialogue();
    }

    public override void ShowDialogue()
    {
        base.ShowDialogue();

        PlayerInteraction.OnInteract.RemoveListener(ShowDialogue);
    }
}