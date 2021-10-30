using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    private void Start()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, 1f);
    }
    private void Update()
    {
        transform.position += transform.right * 10f * Time.deltaTime;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 11)
        {
            Destroy(gameObject);
        }
    }
    private void OnParticleCollision(GameObject other)
    {
        ScoreScript.AddScore(10);
        Destroy(gameObject);
    }
}
