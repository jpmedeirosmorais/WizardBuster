using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;
using TMPro;
using TMPro.Examples;

public class Player : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] private int essence = 0;
    [SerializeField] protected float dashingPower;
    [SerializeField] protected float dashingTime = 0.287f;
    [SerializeField] protected float dashingCooldown = 1f;
    [SerializeField] protected float LifePoints = 100f;
    [SerializeField] protected float attackCooldown = 0.3f;
    [SerializeField] protected GameObject particles;
    [SerializeField] protected GameObject souls;
    [SerializeField] protected GameObject scrolls;
    [SerializeField] protected GameObject pauseMenu;

    private int memorypapers = 0;
    private Rigidbody2D playerRig;
    private Animator playerAnim;
    private SpriteRenderer sprite;
    private TrailRenderer tr;
    private bool isDashing = false;
    private bool canDash = true;
    private bool isAttacking = false;
    private bool canAttack = true;
    public Image healthBar;
    public GameObject gameOverScreen;

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
        pause();
    }

    void UIUpdate()
    {
        souls.GetComponent<TextMeshProUGUI>().text = essence.ToString();
        scrolls.GetComponent<TextMeshProUGUI>().text = memorypapers.ToString();
    }

    private void pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            pauseMenu.SetActive(true);
        }
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
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontal * speed, vertical * speed);
        playerRig.velocity = movement;
        playerAnim.SetBool("Walk", horizontal != 0 || vertical != 0);

        if (horizontal < 0)
        {
            gameObject.transform.localScale = new Vector3(-1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
        else if (horizontal > 0)
        {
            gameObject.transform.localScale = new Vector3(1, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
        }
    }

    private IEnumerator Attacking()
    {
        canAttack = false;
        isAttacking = true;
        playerAnim.SetTrigger("Attack");
        Instantiate(particles, new Vector3(transform.position.x + gameObject.transform.localScale.x, transform.position.y, 0f), Quaternion.identity);
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
        healthBar.fillAmount = LifePoints / 100f;
        if (LifePoints <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Time.timeScale = 0f;
        gameOverScreen.SetActive(true);
    }

    void CollectEssence()
    {
        essence += 2;
        UIUpdate();
        if (essence <= 20)
        {
            attackCooldown -= essence / 100;
        }
    }

    void CollectMemoryPaper(GameObject memoryPaper)
    {
        Destroy(memoryPaper);
        memorypapers++;
        UIUpdate();
        Debug.Log("Total Memory Papers: " + memorypapers);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Enemy":
                if (!isDashing)
                {
                    hit();
                }
                break;
        }
    }



    private void EndGameManager(GameObject exitDoor)
    {
        int memories = GameObject.FindGameObjectsWithTag("MemoryPaper").Length;
        if (memories == 0)
        {
            Debug.Log("Parabéns! Você conseguiu escapar!");
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            Debug.Log("Para abrir a porta é necessário coletar todas as memórias!");
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
            case "ExitDoor":
                EndGameManager(collision.gameObject);
                break;
        }
    }
}
