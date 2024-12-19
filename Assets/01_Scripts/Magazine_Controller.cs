using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine_Controller : MonoBehaviour
{
    public RaycasterScripts rs;
    public SnapInteractable interactable;
    private void Update()
    {
       if(GameObject.FindGameObjectWithTag("Magazine"))
            GetComponent<SnapInteractable>().InjectRigidbody(GameObject.FindGameObjectWithTag("Magazine").GetComponent<Rigidbody>());
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Magazine")
        {
            // 재장전 기능 호출
            Destroy(other.gameObject);
            StartCoroutine(rs.Reroading());
        }
    }
}
