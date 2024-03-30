using UnityEngine;

/// <summary>
/// Use this class if you need more control over the interaction.
/// You'll prob be fine with using only FeedbackInteraction.cs for trigger interaction.
/// </summary>
public class Trigger : MonoBehaviour, IFeedback
{
    [Header("Settings")]
    public bool disableOnInteract;
    //References
    private FeedbackInteraction interaction;

    private void Start()
    {
        //Feedback
        interaction = GetComponent<FeedbackInteraction>();
        SubscribeFeedback();
    }

    public void OnInteract()
    {
        if (disableOnInteract)
            gameObject.SetActive(false);
    }

    public void OnPlayerEnter()
    {

    }

    public void OnPlayerExit()
    {

    }

    public void SubscribeFeedback()
    {
        interaction.OnInteract.AddListener(OnInteract);
        interaction.OnEnter.AddListener(OnPlayerEnter);
        interaction.OnExit.AddListener(OnPlayerExit);
    }
}