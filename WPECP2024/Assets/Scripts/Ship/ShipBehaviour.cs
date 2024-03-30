using UnityEngine;

public enum Galaxies { One, Two, Three}
public class ShipBehaviour : MonoBehaviour, IFeedback
{
    [Header("Tracking Values")]
    public Galaxies currGalaxy;

    //References
    private GalaxyUI galaxy;
    private FeedbackInteraction interaction;

    private void Start()
    {
        //Get references
        galaxy = GalaxyUI.Instance;
        interaction = GetComponent<FeedbackInteraction>();
        //Initial values
        SubscribeFeedback();
    }

    #region Interaction events
    public void OnInteract()
    {
        //Open galaxies UI
        galaxy.ToggleGalaxySelection(true);
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
    #endregion
}