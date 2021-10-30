using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathSplit : MonoBehaviour
{
    [SerializeField] private GameObject left;
    [SerializeField] private GameObject right;
    private void OnEnable()
    {
        StartCoroutine(PushApart());
    }
    IEnumerator PushApart()
    {
        while (right.transform.localScale.x > 0f)
        {
            left.transform.position -= transform.right * 1.5f * Time.deltaTime;
            right.transform.position += transform.right * 1.5f * Time.deltaTime;
            left.transform.localScale -= new Vector3(1f, 1f, 0f) * 0.5f * Time.deltaTime;
            right.transform.localScale -= new Vector3(1f, 1f, 0f) * 0.5f * Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
