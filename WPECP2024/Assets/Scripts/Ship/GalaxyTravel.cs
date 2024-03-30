using UnityEngine;
using UnityEngine.UI;

public class GalaxyTravel : MonoBehaviour
{
    [Header("Settings")]
    public Resources travelCost;
    private int currGalaxy;
    public GameObject[] galaxies;
    [Header("UI")]
    public Button[] galaxiesBtn;
    public RectTransform shipImg;

    private void Start()
    {
        //Initial values
        currGalaxy = 0;
        galaxiesBtn[currGalaxy].interactable = false;
        MoveShip(0);
    }

    public void Travel(int galaxy)
    {
        //Check cost
        if(ResourceManager.Instance.stacks.uranium < travelCost.uranium)
        {
            Debug.Log("Nao tem recursos para viajar");
            return;
        }

        //Cost
        ResourceManager.Instance.RemoveFromStack(ResourceType.Uranium, travelCost.uranium);
        //Buttons
        galaxiesBtn[currGalaxy].interactable = true;
        galaxiesBtn[galaxy].interactable = false;
        //Disable current, enable other
        galaxies[currGalaxy].SetActive(false);
        galaxies[galaxy].SetActive(true);
        currGalaxy = galaxy;
        //Move ship
        MoveShip(galaxy);
    }

    private void MoveShip(int to)
    {
        shipImg.anchoredPosition = galaxiesBtn[to].GetComponent<RectTransform>().anchoredPosition;
    }
}