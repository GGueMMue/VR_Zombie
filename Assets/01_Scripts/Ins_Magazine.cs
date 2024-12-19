using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ins_Magazine : MonoBehaviour
{
    public GameObject magazine_Prefab;
    public GameObject magazine_pos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void Ins_Magazin()
    {
        GameObject magazine = Instantiate(magazine_Prefab, magazine_pos.transform.position, magazine_pos.transform.rotation);
        magazine.transform.localScale = new Vector3(0.05f, 0.05f, 0.05f);
        magazine.GetComponent<SnapInteractor>().InjectRigidbody(GameObject.FindGameObjectWithTag("SnapLocation").GetComponent<Rigidbody>());
    }

    // Update is called once per frame
    void Update()
    {

    }
}
