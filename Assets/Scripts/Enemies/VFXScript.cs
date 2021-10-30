using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFXScript : MonoBehaviour
{
    public IEnumerator Kill()
    {
        yield return new WaitForSecondsRealtime(1f);
        Destroy(gameObject);
    }
}
