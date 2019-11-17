using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class treeColor : MonoBehaviour
{

    [ColorUsageAttribute(true, true, 0f, 8f, 0.125f, 3f)]
    public Color treeGlowColor;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", treeGlowColor);

    }

    private void OnValidate()
    {
        GetComponent<Renderer>().sharedMaterial.SetColor("_EmissionColor", treeGlowColor);
    }


}
