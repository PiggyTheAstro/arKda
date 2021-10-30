using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
public class CameraControl : MonoBehaviour
{
    private Camera cam;
    private GameObject player;
    [SerializeField]
    private float maxX;
    [SerializeField]
    private float maxY;
    private bool shaking;
    private Vector3 displacement;
    private float shakePower;
    private float shakeTime;
    private bool shakeCalled = false;
    private PlayerController playerScript;
    private PostProcessVolume postProcess; 
    private Vignette vignette;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.Find("Player");
        shaking = false;
        displacement = new Vector3(0f, 0f, 0f);
        playerScript = player.GetComponent<PlayerController>();
        postProcess = GetComponent<PostProcessVolume>();
        postProcess.profile.TryGetSettings<Vignette>(out vignette);
    }
    public void SetPowerAndTime(float pow, float time)
    {
        shakePower = pow;
        shakeTime = time;
        shaking = true;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        if (shaking)
        {
            if (!shakeCalled)
            {
                StartCoroutine(Shake(shakeTime));
            }
            displacement.x = Random.Range(-shakePower, shakePower);
            displacement.y = Random.Range(-shakePower, shakePower);
        }
        Vector3 basePos;
        basePos = new Vector3(Mathf.Clamp(player.transform.position.x, -maxX, maxX), Mathf.Clamp(player.transform.position.y, -maxY, maxY), 0f);
        cam.transform.position = basePos + displacement;
    }
    IEnumerator Shake(float time)
    {
        shakeCalled = true;
        yield return new WaitForSecondsRealtime(time);
        shaking = false;
        displacement.x = 0;
        displacement.y = 0;
        shakeCalled = false;
    }
}
