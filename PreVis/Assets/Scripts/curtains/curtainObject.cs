using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class curtainObject : MonoBehaviour
{
    public curtainAttributes defaultState;

    public bool HideMaterial;
    public Material hiddenMat;
    public Material defaultMat;


    private Renderer[] childRenders;

    // Start is called before the first frame update
    void Start()
    {
        defaultState.startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnValidate()
    {
        if (childRenders == null)
        {

            childRenders = GetComponentsInChildren<Renderer>();

            
        }

        if (HideMaterial == true)
        {
            childRenders = GetComponentsInChildren<Renderer>();

            foreach ( Renderer render in childRenders){
                render.material = hiddenMat;
            }
        }
       else
        {
            childRenders = GetComponentsInChildren<Renderer>();

            foreach (Renderer render in childRenders)
            {
                render.material = defaultMat;
            }
        }
    }


}
