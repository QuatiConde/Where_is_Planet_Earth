using UnityEngine;

public class PlayerBucket : MonoBehaviour
{
    public static PlayerBucket Instance;

    public bool HasBucket { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        HasBucket = false;
    }

    private void UpdateBucketUI()
    {

    }

    private void PickBucket()
    {
        HasBucket = true;
    }
}