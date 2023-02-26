using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    float horizontalMove;
    public float speed;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 40f;
    public float jumpLimit = 2f;

    Animator myAnim;

    bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        //pointing to the specific RigidBody2D in the chara
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        //associates with typical horizontal inputs (A/D, L/R arrow)

        //Vector3 newPos = transform.position;

        //if (Input.GetKey(KeyCode.D))
        //{ newPos.x += horizontalMove; }
        //if (Input.GetKey(KeyCode.A))
        //{ newPos.x -= horizontalMove; }

       // transform.position = newPos;
        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        {
           // myAnim.SetBool("walking", true);
        }

        else
        {
           // myAnim.SetBool("walking", false);
        }
    }
    void FixedUpdate()
    {
        float moveSpeed = horizontalMove * speed;

        if (jump)
        {
            myBody.AddForce(Vector2.up * jumpLimit, ForceMode2D.Impulse);
            jump = false;
        }
        if (myBody.velocity.y > 0)
        {
            myBody.gravityScale = gravityScale;
        }
        else if (myBody.velocity.y < 0)
        {
            myBody.gravityScale = gravityFall;
        }

        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, castDist);
        Debug.DrawRay(transform.position, Vector2.down, Color.red);

        if (hit.collider != null && hit.transform.name == "Ground")
        {
            grounded = true;
        }
        else
        {
            grounded = false;
        }
        myBody.velocity = new Vector3(moveSpeed, myBody.velocity.y, 0);
    }
}



//raycasting = extending line until hits something (detection)
// null = noone in gamemaker (checking if something is empty in GM2)