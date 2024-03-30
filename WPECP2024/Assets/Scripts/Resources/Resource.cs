using UnityEngine;
using NaughtyAttributes;
using TMPro;

public enum ResourceType { Water, Uranium, Titanium, Seed, Fertilizer }
public class Resource : MonoBehaviour, IFeedback
{
    [Header("Settings")]
    public ResourceType type;
    [ReadOnly] public int currAmount; //How many resources this will start with?
    public int maxAmount;
    [Header("Collect Settings")]
    public bool collectTimer;
    [ShowIf("collectTimer")] public float timeToCollect; //In seconds
    [ReadOnly] [ShowIf("collectTimer")] public float currTimer; //In seconds
    [ReadOnly] [ShowIf("collectTimer")] public bool isCollecting;
    private SpriteFill timerFill;

    //References
    private TMP_Text amountText;
    private FeedbackInteraction interaction;

    private void Start()
    {
        //Get references
        amountText = GetComponentInChildren<TMP_Text>();
        timerFill = GetComponentInChildren<SpriteFill>();
        interaction = GetComponent<FeedbackInteraction>();
        //Initial values
        currAmount = maxAmount;
        timerFill.Toggle(false);
        UpdateUI();
        SubscribeFeedback();
    }

    private void Update()
    {
        if (isCollecting)
        {
            currTimer -= Time.deltaTime;
            //Update timer UI
            timerFill.SetSize(Mathf.InverseLerp(0f, timeToCollect, currTimer));
            if (currTimer < 0)
            {
                currTimer = 0;
                EndCollect();
            }                
        }
    }

    private void CollectInput()
    {
        if (!WaterCondition())
            return;

        if (!collectTimer)
            GiveResource();
        else
            CollectTimer();
    }

    /// <summary>
    /// This will give some resources to the player.
    /// You can add a parameter to tell how many resources you want to give.
    /// </summary>
    private void GiveResource()
    {
        if(!CheckResource())
        {
            return;
        }

        if (!WaterCondition())
            return;

        ResourceManager.Instance.AddToStack(type, 1);
        currAmount--;
        UpdateUI();

        //Debug.Log($"Giving player resource from {gameObject.name} of type {type}. Remaining amount: {currAmount}");

        //Check remaining resources
        if(!CheckResource())
        {
            interaction.Toggle(false);
        }
    }

    private void CollectTimer()
    {
        PlayerInteraction.OnReleaseInteract.AddListener(StopCollect);

        currTimer = timeToCollect;
        isCollecting = true;
        timerFill.Toggle(true);
    }

    private void EndCollect()
    {
        //Timer ended
        GiveResource();

        if (!WaterCondition())
        {
            StopCollect();
            return;
        }

        //Try start again
        if (CheckResource())
        {
            CollectTimer();
        }
        else
        {
            StopCollect();
        }
    }

    private void StopCollect()
    {
        PlayerInteraction.OnReleaseInteract.RemoveListener(StopCollect);

        currTimer = 0;
        isCollecting = false;
        timerFill.Toggle(false);
    }

    /// <summary>
    /// Returns true if resources is available.
    /// </summary>
    /// <returns></returns>
    public bool CheckResource()
    {
        return currAmount > 0;
    }

    private void UpdateUI()
    {
        amountText.text = $"{currAmount}/{maxAmount}";
    }

    private bool WaterCondition()
    {
        if (type == ResourceType.Water)
        {
            //Check if player have bucket
            if (!PlayerBucket.Instance.HasBucket)
            {
                //Debug.Log("Player doesn't have a bucket!");
                return false;
            }

            if (ResourceManager.Instance.stacks.water > 0)
                return false;
        }

        return true;
    }

    #region Interaction events
    public void OnInteract()
    {
        CollectInput();
    }

    public void OnPlayerEnter()
    {
        if (!CheckResource())
        {
            //Debug.Log($"No more resources available in {gameObject.name}");            
            return;
        }
    }

    public void OnPlayerExit()
    {
        if (!CheckResource()) //Avoid comparing tag if no resource is available
            return;

        if (isCollecting)
            StopCollect();
    }

    public void SubscribeFeedback()
    {
        interaction.OnInteract.AddListener(OnInteract);
        interaction.OnEnter.AddListener(OnPlayerEnter);
        interaction.OnExit.AddListener(OnPlayerExit);
    }
    #endregion
}