using UnityEngine;
using NaughtyAttributes;
using TMPro;

public enum ResourceType { Water, Uranium, Titanium, Seed, Fertilizer }
public class Resource : MonoBehaviour
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
    private GameObject feedback;
    private TMP_Text amountText;

    private void Start()
    {
        //Get references
        feedback = transform.Find("InteractionFeedback").gameObject;
        amountText = GetComponentInChildren<TMP_Text>();
        timerFill = GetComponentInChildren<SpriteFill>();
        //Initial values
        currAmount = maxAmount;
        feedback.SetActive(false);
        timerFill.Toggle(false);
        UpdateUI();
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

        ResourceManager.Instance.AddToStack(type, 1);
        currAmount--;
        UpdateUI();

        //Debug.Log($"Giving player resource from {gameObject.name} of type {type}. Remaining amount: {currAmount}");

        //Check remaining resources
        if(!CheckResource())
        {
            PlayerInteraction.OnInteract.RemoveListener(CollectInput);
            feedback.SetActive(false);
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

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckResource())
        {
            //Debug.Log($"No more resources available in {gameObject.name}");            
            return;
        }

        if (other.CompareTag("Player"))
        {
            feedback.SetActive(true);

            PlayerInteraction.OnInteract.AddListener(CollectInput);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckResource()) //Avoid comparing tag if no resource is available
            return;

        if (isCollecting)
            StopCollect();

        if (other.CompareTag("Player"))
        {
            feedback.SetActive(false);

            PlayerInteraction.OnInteract.RemoveListener(CollectInput);
        }
    }
}