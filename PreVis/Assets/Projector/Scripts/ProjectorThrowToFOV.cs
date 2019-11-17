using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectorThrowToFOV : MonoBehaviour
{
    public bool ShowCone;
    public float ThrowRatio = 1;

    [Range(1,180)]
    public float FOV;

    [ColorUsage(true, true)]
    public Color ProjectionTint = Color.white;

    public Texture2D projectionMask;


    public Vector3 ProjectorDimensions;
    public bool customPosition;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Projector>().material.SetColor("_Color", ProjectionTint);
        GetComponent<Projector>().material.SetTexture("_MaskTex", projectionMask);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CalculateFOV()
    {

       FOV = Mathf.Rad2Deg * (2 * Mathf.Atan(.5f * 1 /(ThrowRatio + ThrowRatio/GetComponent<Projector>().aspectRatio)));



        GetComponent<Projector>().fieldOfView = FOV;

        float projectorDistance = GetComponent<Projector>().farClipPlane;

        Vector3 childScale = new Vector3(1,1, ThrowRatio) * (projectorDistance/ThrowRatio)/ GetComponent<Projector>().aspectRatio;

        transform.GetChild(0).localScale = childScale;

    }

    void ProjectorScale()
    {
        transform.GetChild(1).localScale = ProjectorDimensions * .0254f;

        if (customPosition == false)
        {
            transform.GetChild(1).localPosition = new Vector3(0, 0, -transform.GetChild(1).localScale.z / 2);
        }
    }


    private void OnDrawGizmosSelected()
    {
        if (ShowCone == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CalculateFOV();
            ProjectorScale();
        }
    }

    private void OnValidate()
    {

        if (ShowCone == true)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            CalculateFOV();
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        GetComponent<Projector>().material.SetColor("_Color", ProjectionTint);
        GetComponent<Projector>().material.SetTexture("_MaskTex", projectionMask);

    }

}
