using UnityEngine;
using TMPro;

public class PlayerHUD : MonoBehaviour
{
    public TMP_Text resourcesText;

    //References
    private ResourceManager resources;

    private void Start()
    {
        resources = ResourceManager.Instance;
    }

    private void Update()
    {
        UpdateResources();
    }

    private void UpdateResources()
    {
        resourcesText.text = $"<sprite=0>{resources.stacks.titanium} " +
            $"<sprite=1>{resources.stacks.uranium} " +
            $"<sprite=2>{resources.stacks.water} " +
            $"<sprite=3>{resources.stacks.seed}";
    }
}