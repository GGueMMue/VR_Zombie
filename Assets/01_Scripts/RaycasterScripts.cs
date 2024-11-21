using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaycasterScripts : MonoBehaviour
{

    public GameObject muzzleEffect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch))
        {
            Debug.Log("Ray น฿ป็");
            muzzleEffect.GetComponent<ParticleSystem>().Play();
            //Ray ray;
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);

                if(hit.collider.gameObject.CompareTag("Target"))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }
}
