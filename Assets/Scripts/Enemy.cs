using UnityEngine;

public class Enemy : MonoBehaviour
{

    [SerializeField] protected float speed;
    [SerializeField] protected float attackRange;
    private GameObject player;
    private Rigidbody2D enemyRig;

    void Start()
    {
        enemyRig = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    void Update()
    {
        Attack();
    }

    private void FixedUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        float random = Random.Range(0, 100);

        Vector2 direction = (player.transform.position - transform.position) * random;
        enemyRig.velocity = direction.normalized * speed;
    }

    private void Attack()
    {
        bool isInRange = Physics2D.OverlapCircle(transform.position, attackRange, LayerMask.GetMask("Player"));

        if (isInRange)
        {
            Debug.DrawLine(transform.position, player.transform.position, Color.red);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
