using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    float horizontalMove;
    public float speed;

    Rigidbody2D myBody;

    bool grounded = false;

    public float castDist = 0.2f;
    public float gravityScale = 5f;
    public float gravityFall = 10f;
    public float jumpLimit = 2f;

    bool keyObtain = false;

    //public GameObject doorText;
    //public GameObject keyObtained;


    Animator myAnim;

    bool jump = false;


    // Start is called before the first frame update
    void Start()
    {
        
        //keyObtained.SetActive(false);
       //doorText.SetActive(false);
        myBody = GetComponent<Rigidbody2D>();
        //pointing to the specific RigidBody2D in the chara
        myAnim = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
 
       if (Input.GetButtonDown("Jump"))
        {
            print("a");
        }

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
        }
        //if (horizontalMove > 0.2f || horizontalMove < -0.2f)
        //{
           // myAnim.SetBool("walking", true);
       // }

        //else
       // {
           // myAnim.SetBool("walking", false);
       // }
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.name == "key")
        {
            keyObtain = true;
            Destroy(collision.gameObject);
        }

        if (keyObtain && collision.gameObject.name == "door") 
            {
            //go to next room if key obtained and person collides w door
            // SceneManager.GetSceneByName("Level2");

            SceneManager.LoadScene("Level2");
            //SceneManager.LoadScene(2);
        } 
    }
}



//raycasting = extending line until hits something (detection)
// null = noone in gamemaker (checking if something is empty in GM2)