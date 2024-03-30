using UnityEngine;

public class GalaxyUI : MonoBehaviour
{
    public static GalaxyUI Instance { get; private set; }

    [Header("References")]
    public GameObject galaxySelection;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }

        DontDestroyOnLoad(this);
    }

    private void Start()
    {
        //Initial values
        ToggleGalaxySelection(false);
    }

    public void ToggleGalaxySelection(bool toggle)
    {
        galaxySelection.SetActive(toggle);

        PlayerMovement.Instance.ToggleMovement(!toggle);
    }
}