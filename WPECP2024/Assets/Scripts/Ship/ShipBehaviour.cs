using UnityEngine;

public enum Galaxies { One, Two, Three, Four, Five}
public class ShipBehaviour : MonoBehaviour
{
    [Header("Tracking Values")]
    public Galaxies currGalaxy;

    //References
    private GalaxyUI galaxy;
    private GameObject feedback;

    private void Start()
    {
        //Get references
        galaxy = GalaxyUI.Instance;
        feedback = transform.Find("InteractionFeedback").gameObject;
        //Initial values
        feedback.SetActive(false);
    }

    private void OnInteract()
    {
        //Open galaxies UI
        galaxy.ToggleGalaxySelection(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedback.SetActive(true);

            PlayerInteraction.OnInteract.AddListener(OnInteract);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedback.SetActive(false);

            PlayerInteraction.OnInteract.AddListener(OnInteract);
        }
    }
}