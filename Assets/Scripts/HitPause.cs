using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitPause : MonoBehaviour
{
    public void Pause(float time)
    {
        StartCoroutine(Unpause(time));
        Time.timeScale = 0.01f;
    }
    private IEnumerator Unpause(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        Time.timeScale = 1f;
    }
}
