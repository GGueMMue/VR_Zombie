using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ins_Magazine : MonoBehaviour
{
    public GameObject magazine_Prefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameObject.FindGameObjectWithTag("Magazine"))
        {
            Instantiate(magazine_Prefab, transform.position, transform.rotation);
        }
        else return;
    }
}
