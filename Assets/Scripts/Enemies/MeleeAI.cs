using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAI : MonoBehaviour
{
    GameObject player;
    HitResponse hitResponse;
    EnemyHealth health;
    [SerializeField] float speed;
    void Start()
    {
        player = GameObject.Find("Player");
        hitResponse = GetComponent<HitResponse>();
        health = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
        Vector3 direction = (player.transform.position - transform.position).normalized;
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 6);
        direction += Alignment(col) + Cohesion(col) + Separation(col) * 1.3f;
        direction = direction.normalized;
        if (!hitResponse.Hit())
        {
            transform.position += direction.normalized * Time.deltaTime * speed;
        }
        transform.rotation = Quaternion.Euler(0f, 0f, -90 + Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
    }
    Vector3 Alignment(Collider2D[] col)
    {
        Vector3 dir = new Vector3(0f, 0f, 0f);
        int neighbours = 0;
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.layer == 10)
            {
                neighbours++;
                dir += col[i].gameObject.transform.up;
            }
        }
        if(neighbours < 1)
        {
            return Vector3.zero;
        }
        dir /= neighbours;
        return dir.normalized;
    }
    Vector3 Cohesion(Collider2D[] col)
    {
        Vector3 dir = new Vector3(0f, 0f, 0f);
        int neighbours = 0;
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.layer == 10)
            {
                neighbours++;
                dir += col[i].gameObject.transform.position;
            }
        }
        if (neighbours < 1)
        {
            return Vector3.zero;
        }
        dir /= neighbours;
        dir -= transform.position;
        return dir.normalized;
    }
    Vector3 Separation(Collider2D[] col)
    {
        Vector3 dir = new Vector3(0f, 0f, 0f);
        int neighbours = 0;
        for (int i = 0; i < col.Length; i++)
        {
            if (col[i].gameObject.layer == 10)
            {
                neighbours++;
                dir += col[i].gameObject.transform.position - transform.position;
            }
        }
        if (neighbours < 1)
        {
            return Vector3.zero;
        }
        dir /= neighbours;
        dir *= -1f;
        return dir.normalized;
    }
    private void OnParticleCollision(GameObject other)
    {
        hitResponse.Hit(other.transform.up, 0.2f, 6, 12);
        health.DecrementHealth(2f, 666);
    }
}
