using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycasterScripts : MonoBehaviour
{

    public GameObject muzzleEffect;
    public TextMeshProUGUI ammoText;
    public GameObject bulletEffect;

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
            Debug.Log("Ray �߻�");
            Instantiate(muzzleEffect, this.transform.position, this.transform.rotation);

            bulletCnt--;
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
                else
                {
                    Quaternion rotation = Quaternion.LookRotation(-hit.normal); // hit�� �ݶ��̴� ���� ���͸� ����ȭ �� ���� -�� �־� �ڸ� ���� ��
                    Instantiate(bulletEffect, hit.point, rotation); // ���� ��ġ�� �ش� ���� ����
                    
                }
            }
        }
    }

    public IEnumerator Reroading()
    {
        yield return new WaitForSeconds(1f);
        bulletCnt = 7;

        yield return null;
    }
}
