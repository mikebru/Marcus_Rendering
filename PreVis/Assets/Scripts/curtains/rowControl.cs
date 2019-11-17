using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rowControl : MonoBehaviour
{

    [Range (0, 360)]
    public float rotation;

    public curtainObject[] curtains;
    public bool RotateinTime;

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        curtains = GetComponentsInChildren<curtainObject>();
    }



    private void OnValidate()
    {
        if(curtains.Length == 0)
        {
            Start();
        }

        RotateAll();
    }


    void RotateAll()
    {
        for(int i=0; i< curtains.Length; i++)
        {

            curtains[i].transform.rotation = Quaternion.Euler(Vector3.up * rotation);

        }

    }


    // Update is called once per frame
    void Update()
    {
        
        if(RotateinTime == true)
        {
            for (int i = 0; i < curtains.Length; i++)
            {

                curtains[i].transform.Rotate(Vector3.up * speed);

            }
        }

    }
}
