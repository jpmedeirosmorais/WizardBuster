using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float speed;
    private Rigidbody playerRig;
    [SerializeField] protected float JumpForce;
    [SerializeField] protected bool isJumping;


    void Start()
    {
        playerRig = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -50f, 0f);
    }

    void Update()
    {
        Actions();
    }

    

    private void Actions()
    {
        Walk();
        Jump();
        Attack();
    }

    private void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal*speed, 0f ,vertical * speed);
        playerRig.velocity= movement;
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            isJumping = true;
            playerRig.AddForce(new Vector3(0f, JumpForce, 0f), ForceMode.Impulse);
        }
    }

    private void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.layer)
        {
            case 6:
                isJumping = false;
                break;
        }
    }
}
