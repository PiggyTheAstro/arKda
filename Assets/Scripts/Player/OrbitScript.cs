using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitScript : MonoBehaviour
{
    private float currentAngle;
    [SerializeField] private int id;
    private GameObject player;
    private PlayerController controls;
    bool activated;
    private void Start()
    {
        player = GameObject.Find("Player");
        controls = player.GetComponent<PlayerController>();
        activated = false;
    }
    void Update()
    {
        if (activated)
        {
            if (id != 2)
            {
                currentAngle += 4f * Time.deltaTime;
            }
            else
            {
                currentAngle -= 4f * Time.deltaTime;
            }
            Vector3 offset = new Vector2(Mathf.Sin(currentAngle), Mathf.Cos(currentAngle)) * 5f;
            transform.position = player.transform.position + offset;
        }
        else
        {
            transform.position = new Vector3(1000.0f, 1000.0f, -1000.0f);
        }
        if(id > controls.Lives())
        {
            activated = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer == 10)
        {
            col.gameObject.GetComponent<HitResponse>().Hit(transform.up, 0.15f, 12, 15);
            col.gameObject.GetComponent<EnemyHealth>().DecrementHealth(4f, 666);
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.layer == 12)
        {
            Destroy(col.gameObject);
        }
    }
}
