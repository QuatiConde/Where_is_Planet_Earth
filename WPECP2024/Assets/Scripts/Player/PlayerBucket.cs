using UnityEngine;

public class PlayerBucket : MonoBehaviour
{
    public static PlayerBucket Instance;

    public bool HasBucket { get; private set; }

    public FeedbackInteraction interaction;    

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //Initial values
        HasBucket = false;
        //Subscribe events
        interaction.OnInteract.AddListener(PickBucket);
    }

    private void UpdateBucketUI()
    {

    }

    private void PickBucket()
    {
        HasBucket = true;
    }
}