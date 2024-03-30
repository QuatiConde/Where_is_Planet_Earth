using UnityEngine;
using UnityEngine.Events;

public class PlayerShovel : MonoBehaviour
{
    public static PlayerShovel Instance;

    public bool HasShovel { get; private set; }
    public FeedbackInteraction interaction;

    public static UnityEvent OnGetShovel = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //Initial values
        HasShovel = false;
        //Subscribe events
        interaction.OnInteract.AddListener(PickShovel);
    }

    public void PickShovel()
    {
        OnGetShovel?.Invoke();
        HasShovel = true;
    }
}