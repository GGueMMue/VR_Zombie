using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShaker : MonoBehaviour
{
    public Vector3 myLocalPos;

    void Start()
    {
        myLocalPos = transform.localPosition;
    }

    IEnumerator CameraShakeProcess(float shakeTime, float shakeSence)
    {
        float curTime = 0;
        while (curTime < shakeTime)
        {
            curTime += Time.deltaTime;
            transform.localPosition = myLocalPos;

            Vector3 pos = Vector3.zero;
            pos.x = Random.Range(-shakeSence, shakeSence);
            pos.y = Random.Range(-shakeSence, shakeSence);
            pos.z = Random.Range(-shakeSence, shakeSence);

            transform.localPosition += pos;

            yield return new WaitForEndOfFrame();
        }
        transform.localPosition = myLocalPos;
    }

    public void PlayCameraShake()
    {
        StartCoroutine(CameraShakeProcess(0.5f, 0.5f));
    }
}