using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class ResourceDeposit : MonoBehaviour, IFeedback
{
    //References
    public Resources neededResource;
    private ResourceManager playerResources;
    private FeedbackInteraction interaction;
    private TMP_Text amountText;

    //Events
    public UnityEvent OnComplete;

    private void Start()
    {
        //Get references
        amountText = GetComponentInChildren<TMP_Text>();
        playerResources = ResourceManager.Instance;
        //Initial values
        UpdateAmountText();
        //Feedback
        interaction = GetComponent<FeedbackInteraction>();
        SubscribeFeedback();
    }

    private void UpdateAmountText()
    {
        amountText.text = "";

        if (neededResource.titanium > 0)
            amountText.text += $"<sprite=0>{neededResource.titanium} ";

        if (neededResource.uranium > 0)
            amountText.text += $"<sprite=1>{neededResource.uranium} ";

        if (neededResource.water > 0)
            amountText.text += $"<sprite=2>{neededResource.water} ";

        if (neededResource.seed > 0)
            amountText.text += $"<sprite=3>{neededResource.seed} ";

        if (neededResource.fertilizer > 0)
            amountText.text += $"<sprite=4>{neededResource.fertilizer} ";
    }

    private void SubtractResource(ResourceType type, int amount)
    {
        switch (type)
        {
            case ResourceType.Water:
                neededResource.water -= amount;
                playerResources.RemoveFromStack(ResourceType.Water, amount);
                break;
            case ResourceType.Uranium:
                neededResource.uranium -= amount;
                playerResources.RemoveFromStack(ResourceType.Uranium, amount);
                break;
            case ResourceType.Titanium:
                neededResource.titanium -= amount;
                playerResources.RemoveFromStack(ResourceType.Titanium, amount);
                break;
            case ResourceType.Seed:
                neededResource.seed -= amount;
                playerResources.RemoveFromStack(ResourceType.Seed, amount);
                break;
            case ResourceType.Fertilizer:
                neededResource.fertilizer -= amount;
                playerResources.RemoveFromStack(ResourceType.Fertilizer, amount);
                break;
        }

        UpdateAmountText();
        CheckRemaining();
    }

    private void CheckRemaining()
    {
        if (neededResource.titanium > 0)
            return;

        if (neededResource.uranium > 0)
            return;

        if (neededResource.water > 0)
            return;

        if (neededResource.seed > 0)
            return;

        if (neededResource.fertilizer > 0)
            return;

        OnComplete?.Invoke();
    }

    #region Interaction events
    public void OnInteract()
    {
        if(playerResources.stacks.titanium > 0 && neededResource.titanium > 0)
        {
            if (playerResources.stacks.titanium - neededResource.titanium < 0)
                SubtractResource(ResourceType.Titanium, playerResources.stacks.titanium);
            else
                SubtractResource(ResourceType.Titanium, neededResource.titanium);
        }

        if (playerResources.stacks.uranium > 0 && neededResource.uranium > 0)
        {
            if (playerResources.stacks.uranium - neededResource.uranium < 0)
                SubtractResource(ResourceType.Uranium, playerResources.stacks.uranium);
            else
                SubtractResource(ResourceType.Uranium, neededResource.uranium);
        }

        if (playerResources.stacks.water > 0 && neededResource.water > 0)
        {
            if (playerResources.stacks.water - neededResource.water < 0)
                SubtractResource(ResourceType.Water, playerResources.stacks.water);
            else
                SubtractResource(ResourceType.Water, neededResource.water);
        }

        if (playerResources.stacks.seed > 0 && neededResource.seed > 0)
        {
            if (playerResources.stacks.seed - neededResource.seed < 0)
                SubtractResource(ResourceType.Seed, playerResources.stacks.seed);
            else
                SubtractResource(ResourceType.Seed, neededResource.seed);
        }

        if (playerResources.stacks.fertilizer > 0 && neededResource.fertilizer > 0)
        {
            if (playerResources.stacks.fertilizer - neededResource.fertilizer < 0)
                SubtractResource(ResourceType.Fertilizer, playerResources.stacks.fertilizer);
            else
                SubtractResource(ResourceType.Fertilizer, neededResource.fertilizer);
        }
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