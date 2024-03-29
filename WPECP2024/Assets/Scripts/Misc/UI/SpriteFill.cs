using UnityEngine;

public class SpriteFill : MonoBehaviour
{
    private float maxFill; //Min is always 0
    //References
    private SpriteRenderer fill;

    private void Start()
    {
        //Get referecens
        fill = GetComponent<SpriteRenderer>();
        //Initial values
        maxFill = fill.size.x;
    }

    public void Toggle(bool value)
    {
        transform.parent.gameObject.SetActive(value);
    }

    public void SetSize(float percentage)
    {
        float clamped = 0 + (percentage * (maxFill - 0));
        fill.size = new(clamped, fill.size.y);
    }
}