using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionSphere_Y : MonoBehaviour
{
    private float timer;
    [Range(0.1f, 100f)] public float targetScale;
    private Vector3 scale = new Vector3(1f, 1f, 1f);
    public float deleteTime;
    private bool isScaling = false;

    // Update is called once per frame
    void Update()
    {
        if (isScaling)
        {
            timer += Time.deltaTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, scale * targetScale, timer / deleteTime);
            if (timer >= deleteTime) Destroy(gameObject);
        }
    }

    public void SetScalingFlg(float timing)
    {
        StartCoroutine(FlgOn(timing));
    }

    private IEnumerator FlgOn(float timing)
    {
        yield return new WaitForSeconds(timing);
        isScaling = true;
    }
}
