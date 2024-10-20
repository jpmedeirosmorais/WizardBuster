using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] private int Essence = 0;
    [SerializeField] protected float dashingPower;
    [SerializeField] protected float dashingTime = 0.287f;
    [SerializeField] protected float dashingCooldown = 1f;
    [SerializeField] protected float LifePoints = 100f;
    [SerializeField] protected float attackCooldown = 0.2f;
    [SerializeField] protected GameObject particles;

    private int memorypapers = 0;
    private Rigidbody2D playerRig;
    private Animator playerAnim;
    private SpriteRenderer sprite;
    private TrailRenderer tr;
    private bool isDashing = false;
    private bool canDash = true;
    private bool isAttacking = false;
    private bool canAttack = true;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        playerRig = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<Animator>();
        tr = GetComponent<TrailRenderer>();
    }

    void Update()
    {
        limitSpeed();
        Actions();
    }

    void limitSpeed()
    {
        float limitSpeedX = 10f;
        float limitSpeedY = 7f;

        if (playerRig.velocity.x > limitSpeedX)
        {
            playerRig.velocity = new Vector2(limitSpeedX, playerRig.velocity.y);
        }
        else if (playerRig.velocity.y > limitSpeedY)
        {
            playerRig.velocity = new Vector2(playerRig.velocity.x, limitSpeedY);
        }
    }

    private void Actions()
    {
        if (isDashing)
        {
            return;
        }
        Walk();
        Attack();
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            playerAnim.SetTrigger("Dash");
            StartCoroutine(Dashing());
        }
    }

    private void Attack()
    {
        if (Input.GetMouseButtonDown(0) && canAttack)
        {
            StartCoroutine(Attacking());
        }
    }

    private void Walk()
    {

        if (isAttacking)
        {
            playerAnim.SetBool("Walk", false);
            playerRig.velocity = Vector2.zero;
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
        playerRig.velocity = movement;
        playerAnim.SetBool("Walk", horizontal != 0 || vertical != 0);

        if (horizontal < 0)
        {
            sprite.flipX = true;
        }
        else if (horizontal > 0)
        {
            sprite.flipX = false;
        }
    }

    private IEnumerator Attacking()
    {
        canAttack = false;
        isAttacking = true;
        playerAnim.SetTrigger("Attack");
        Instantiate(particles, new Vector3(transform.position.x, transform.position.y, 0f), Quaternion.identity);
        yield return new WaitForSeconds(attackCooldown);
        isAttacking = false;
        yield return new WaitForSeconds(attackCooldown);
        canAttack = true;
    }

    private IEnumerator Dashing()
    {
        canDash = false;
        isDashing = true;
        playerRig.AddForce(playerRig.velocity * dashingPower, ForceMode2D.Impulse);
        tr.emitting = true;
        yield return new WaitForSeconds(dashingTime);
        tr.emitting = false;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }

    void hit()
    {
        LifePoints = LifePoints - 10f;
        if (LifePoints <= 0)
        {
            Destroy(gameObject);
        }
    }

    void CollectEssence()
    {
        Essence += 5;
        if (Essence % 10 == 0)
        {
            LifePoints += 20f;
        }

        Debug.Log("Total Essence: " + Essence);
    }

    void CollectMemoryPaper(GameObject memoryPaper)
    {
        Destroy(memoryPaper);
        memorypapers++;
        Debug.Log("Total Memory Papers: " + memorypapers);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                hit();
                break;
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Essence":
                CollectEssence();
                Destroy(collision.gameObject);
                break;
            case "MemoryPaper":
                CollectMemoryPaper(collision.gameObject);
                break;
        }
    }
}
