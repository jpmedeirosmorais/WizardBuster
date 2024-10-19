using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float originalSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float detectRange;
    private bool seguindojogador = false;
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
        } else
        {
            speed = 0f;
            Vector2 direction = (player.transform.position - transform.position);
            enemyRig.velocity = direction.normalized * speed;
        }
    }

    private void Attack()
    {
        var distanciajogador = Vector2.Distance(player.transform.position, transform.position);
        if (distanciajogador <= attackRange)
        {
            speed = 0f;
        } else
        {
            speed = originalSpeed;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
