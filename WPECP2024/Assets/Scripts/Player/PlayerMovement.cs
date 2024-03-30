using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement Instance { get; private set; }

    public float speed;
    public bool IsMoving { get; private set; }
    public bool CanMove { get; private set; }

    //Internal values
    private Vector3 moveVector;
    //References
    private CharacterController controller;
    private Animator anim;
    private SpriteRenderer sprite;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    private void Start()
    {
        //Get references
        controller = GetComponent<CharacterController>();
        sprite = transform.Find("Sprite").GetComponent<SpriteRenderer>();
        anim = sprite.GetComponent<Animator>();
        //Initial values
        ToggleMovement(true);
    }

    private void Update()
    {
        if (CanMove)
        {
            Move();
            AnimationParameters();
            SetFacingDirection();
        }        
    }

    public void ToggleMovement(bool toggle)
    {
        CanMove = toggle;

        if (!toggle)
            anim.SetBool("IsMoving", false);
    }

    private void Move()
    {
        //moveVector = new(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //moveVector = Vector3.ClampMagnitude(moveVector, 1f); //Clamp so diagonal speed is not faster

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

    public void OnMoveInput(InputAction.CallbackContext ctx)
    {
        Vector2 v = ctx.ReadValue<Vector2>();
        //Move only in X and Y
        moveVector.x = v.x;
        moveVector.z = v.y;
        moveVector = Vector3.ClampMagnitude(moveVector, 1f); //Clamp so diagonal speed is not faster
    }
}