using UnityEngine;

public class ShipBehaviour : MonoBehaviour
{
    private GameObject feedback;

    private void Start()
    {
        //Get references
        feedback = transform.Find("InteractionFeedback").gameObject;
        //Initial values
        feedback.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            feedback.SetActive(true);
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