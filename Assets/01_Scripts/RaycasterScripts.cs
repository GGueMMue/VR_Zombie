using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaycasterScripts : MonoBehaviour
{

    public GameObject muzzleEffect;
    public TextMeshProUGUI ammoText;
    public GameObject bulletEffect;
    public AudioClip shotSFX;

    public Ins_Magazine Ins_Magazine;

    public int bulletCnt;
    float curTime;

    float rpm = 0.15f;

    // Start is called before the first frame update
    void Start()
    {
        bulletCnt = 7;
        curTime = rpm;
    }

    // Update is called once per frame
    void Update()
    {
        curTime += Time.deltaTime;
        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger, OVRInput.Controller.RTouch) && bulletCnt > 0 && curTime >= rpm)
        {
            curTime = 0;
            Debug.Log("Ray 발사");
            AudioManager.Instance().PlaySfx(shotSFX);
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
                    Quaternion rotation = Quaternion.LookRotation(-hit.normal); // hit의 콜라이더 방향 벡터를 정규화 한 값에 -를 넣어 뒤를 보게 함
                    Instantiate(bulletEffect, hit.point, rotation); // 맞은 위치에 해당 값을 넣음
                    
                }
            }
        }
    }


    public IEnumerator Reroading()
    {
        yield return new WaitForSeconds(1f);
        bulletCnt = 7;

        Ins_Magazine.Ins_Magazin();

        yield return null;
    }
}
