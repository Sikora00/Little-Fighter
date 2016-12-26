using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{
    // Use this for initialization
    private Vector3 translation;
    private Vector3 playerPosition;
    private Vector3 spawnerPosition;
    public float Speed = 10;
    public float Force = 300;
    Rigidbody2D rigidbody;
    private Animator anim;
    private SpriteRenderer mySpriteRenderer;
    private Transform groundCheck;
    [SerializeField]    private LayerMask whatIsGround; // A mask determining what is ground to the character
    private float groundedRadius = 0.2f; // Radius of the overlap circle to determine if grounded
    private bool grounded = false; // Whether or not the player is grounded.

    void Start()
    {
        translation = Vector3.right;
        playerPosition = new Vector3(0, 0, 0);
        spawnerPosition = new Vector3();
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        mySpriteRenderer = GetComponent<SpriteRenderer>();
        groundCheck = transform.Find("GroundCheck");
    }

    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
    }
    // Update is called once per frame
    void Update()
    {
        

        if (Input.GetKeyDown(KeyCode.Space) && grounded)
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(new Vector2(0, Force));
            anim.SetFloat("velocity", 10);
            anim.Play("Jump");
        }

        translation.x = Input.GetAxis("Horizontal") * Time.deltaTime
            * Speed;
        if (translation.x != 0)
        {
            if (translation.x < 0) mySpriteRenderer.flipX = true;
            else mySpriteRenderer.flipX = false;
            playerPosition = transform.position;
            transform.Translate(translation);
            playerPosition.x = (Mathf.Clamp(transform.position.x, Preferences.LeftBorder,
                Preferences.RightBorder));
            transform.position = playerPosition;
            if(grounded) anim.Play("Run");
        }
    }
}
