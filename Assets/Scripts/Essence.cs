using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Essence : MonoBehaviour
{
    [SerializeField] protected float detectRange;
    [SerializeField] protected float speed = 2;
    [SerializeField] protected float aceleration = 0;
    private Rigidbody2D rig;
    private GameObject player;
    void Start()
    {
        player = GameObject.Find("Player");
        rig = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        followPlayer();
    }

    void followPlayer()
    {
        var distanciajogador = Vector2.Distance(player.transform.position, transform.position);
        aceleration = aceleration > 20 ? 20 : aceleration + .5f + Time.deltaTime;
        if (distanciajogador <= detectRange)
        {
            float random = Random.Range(0, 100);

            Vector2 direction = (player.transform.position - transform.position) * random;
            rig.velocity = direction.normalized * (speed + aceleration);
        }
    }
}
