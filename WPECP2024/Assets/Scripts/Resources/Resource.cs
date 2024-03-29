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

    //References
    private GameObject feedback;
    private TMP_Text amountText;

    private void Start()
    {
        //Get references
        feedback = transform.Find("InteractionFeedback").gameObject;
        amountText = GetComponentInChildren<TMP_Text>();
        //Initial values
        currAmount = maxAmount;
        feedback.SetActive(false);
        UpdateUI();
    }

    /// <summary>
    /// This will give some resources to the player.
    /// You can add a parameter to tell how many resources you want to give.
    /// </summary>
    private void GiveResource()
    {
        if(currAmount > 0)
        {
            ResourceManager.Instance.AddToStack(type, 1);
            currAmount--;
            UpdateUI();

            //Debug.Log($"Giving player resource from {gameObject.name} of type {type}. Remaining amount: {currAmount}");
        }
    }

    private void UpdateUI()
    {
        amountText.text = $"{currAmount}/{maxAmount}";
    }

    private void OnTriggerEnter(Collider other)
    {
        if (currAmount == 0)
        {
            //Debug.Log($"No more resources available in {gameObject.name}");
            return;
        }

        if (other.CompareTag("Player"))
        {
            feedback.SetActive(true);

            GiveResource();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedback.SetActive(false);
        }
    }
}