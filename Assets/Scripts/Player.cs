using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float speed;


    void Start()
    {

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

        Vector2 movement = new Vector2(horizontal, vertical);
        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void Jump()
    {
        
    }

    private void Attack()
    {

    }
}
