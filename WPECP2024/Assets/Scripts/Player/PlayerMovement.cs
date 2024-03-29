using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public bool IsMoving { get; private set; }

    //Internal values
    private Vector3 moveVector;
    //References
    private CharacterController controller;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Start()
    {
        //Get references
        controller = GetComponent<CharacterController>();
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        anim = sprite.GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        AnimationParameters();
        SetFacingDirection();
    }

    private void Move()
    {
        moveVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        moveVector = Vector3.ClampMagnitude(moveVector, 1f); //Clamp so diagonal speed is not faster

        controller.Move(speed * Time.deltaTime * moveVector);
        
        IsMoving = moveVector != Vector3.zero;
    }

    private void AnimationParameters()
    {
        anim.SetBool("IsMoving", IsMoving);
    }

    private void SetFacingDirection()
    {
        if (moveVector.x > 0)
            sprite.flipX = false;
        else if (moveVector.x < 0)
            sprite.flipX = true;
    }
}