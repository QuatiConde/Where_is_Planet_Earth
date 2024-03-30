using UnityEngine;

public class Hole : MonoBehaviour
{
    public int currStage;
    [Header("References")]
    public GameObject[] holeSprites;
    public GameObject[] treeSprites;

    //References
    private ResourceDeposit deposit;

    private void Start()
    {
        //Get references
        deposit = transform.parent.GetComponent<ResourceDeposit>();
        //Initial values
        currStage = -1;
        CloseHole();
        //Subscribe to events
        deposit.OnComplete.AddListener(NextStage);
        PlayerShovel.OnGetShovel.AddListener(() => deposit.canInteract = true);
    }

    [ContextMenu("Debug")]
    public void NextStage()
    {
        if (!PlayerShovel.Instance.HasShovel)
        {
            Debug.Log("Player nao tem a pa");
            return;
        }

        if (currStage == 2)
            return;

        currStage++;
        SetTree(currStage);
        OpenHole();

        if (currStage == 2)
            return;

        deposit.neededResource.seed = 1;
        deposit.neededResource.water = 1;
        deposit.UpdateAmountText();
    }

    public void OpenHole()
    {
        holeSprites[1].SetActive(true);
        holeSprites[0].SetActive(false);
    }

    public void CloseHole()
    {
        holeSprites[0].SetActive(true);
        holeSprites[1].SetActive(false);
    }

    public void SetTree(int stage)
    {
        for (int i = 0; i < treeSprites.Length; i++)
        {
            treeSprites[i].SetActive(false);
        }

        treeSprites[stage].SetActive(true);
    }
}