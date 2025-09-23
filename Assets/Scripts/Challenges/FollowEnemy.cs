using System;
using UnityEngine;

public class FollowEnemy : MonoBehaviour
{
    public float moveSpeed;

    public Transform target;
    public float sigthRange;
    public float chaseRange;
    public bool canChase;

    private Vector2 _movedirection;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody2D.linearVelocityX = canChase ? _movedirection.x * moveSpeed : 0;
    }

    private void Update()
    {
        if (target == null) return;
        _movedirection = Vector3.Normalize(target.position - transform.position);

        if (Vector2.Distance(target.position, transform.position) < sigthRange)
        {
            canChase = true;
        }
        else if(Vector2.Distance(target.position, transform.position) > chaseRange)
        {
            canChase = false;
        }

        transform.localScale = transform.position.x > target.position.x ? new Vector2(1, 1) : new Vector2(-1, 1);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.chartreuse;
        Gizmos.DrawWireSphere(transform.position, sigthRange);
        Gizmos.color = Color.aquamarine;
        Gizmos.DrawWireSphere(transform.position, chaseRange);
    }
}
