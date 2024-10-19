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
        Attack();
    }

    private void Walk()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal*speed, 0f ,vertical * speed);
        playerRig.velocity= movement;
    }

    private void Attack()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
