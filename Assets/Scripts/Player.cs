using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float speed;
    private Rigidbody2D playerRig;
    private Animator playerAnim;


    void Start()
    {
        playerRig = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        Physics.gravity = new Vector3(0f, -50f, 0f);
    }

    void Update()
    {
        Actions();
    }

    

    private void Actions()
    {
        Walk();
        Attack();
    }

    private void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal*speed,vertical * speed);
        playerRig.velocity= movement;
        playerAnim.SetBool("Walk", horizontal != 0 || vertical != 0);
    }

    private void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
