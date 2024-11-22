using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magazine_Controller : MonoBehaviour
{
    public RaycasterScripts rs;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Magazine")
        {
            // ������ ��� ȣ��
            StartCoroutine(rs.Reroading());
            Destroy(other.gameObject, 1f);
        }
    }
}
