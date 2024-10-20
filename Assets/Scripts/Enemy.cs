using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float originalSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float detectRange;
    [SerializeField] protected float LifePoints = 10;
    [SerializeField] private GameObject prefabEssence;
    private GameObject player;
    private Rigidbody2D enemyRig;

    void Start()
    {
        originalSpeed = speed;
        enemyRig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {

    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var distanciajogador = Vector2.Distance(player.transform.position, transform.position);
        if (distanciajogador <= detectRange)
        {
            float random = Random.Range(0, 100);

            Vector2 direction = (player.transform.position - transform.position) * random;
            enemyRig.velocity = direction.normalized * speed;
            Attack();
        }
        else
        {
            speed = 0f;
            Vector2 direction = player.transform.position - transform.position;
            enemyRig.velocity = direction.normalized * speed;
        }

        if (enemyRig.velocity.x <= -0.01f)
        {
            transform.localScale = new Vector2(-1f, 1f);
        }
        else if (enemyRig.velocity.x >= 0.01f)
        {
            transform.localScale = new Vector2(1f, 1f);
        }
    }

    private void Attack()
    {
        var distanciajogador = Vector2.Distance(player.transform.position, transform.position);
        if (distanciajogador <= attackRange)
        {
            speed = 0f;
        }
        else
        {
            speed = originalSpeed;
        }
    }

    void Hit()
    {
        LifePoints -= 10;
        int random = Random.Range(1, 2);
        if (LifePoints <= 0)
        {
            Destroy(this.gameObject);
            Instantiate(prefabEssence, new Vector2(transform.position.x + random, transform.position.y + random), Quaternion.identity);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "PlayerHit":
                Hit();
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectRange);
    }
}
