using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycasterScripts : MonoBehaviour
{

    public GameObject muzzleEffect;
    public TextMeshProUGUI ammoText;

    public int bulletCnt;

    // Start is called before the first frame update
    void Start()
    {
        bulletCnt = 7;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && bulletCnt > 0)
        {
            Debug.Log("Ray น฿ป็");
            muzzleEffect.GetComponent<ParticleSystem>().Play();
            //bulletCnt--;
            ammoText.text = bulletCnt.ToString();
            //Ray ray;
            RaycastHit hit;

            if(Physics.Raycast(transform.position, transform.forward, out hit))
            {
                Debug.Log(hit.collider.gameObject.name);

                if(hit.collider.gameObject.CompareTag("Enemy"))
                {
                    hit.collider.gameObject.GetComponent<EnemyFSMNavMesh>().Damaged();
                }
            }
        }
    }
}
