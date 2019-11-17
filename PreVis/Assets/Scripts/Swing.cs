using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swing : MonoBehaviour
{

    public Vector3 swingForce;

    [Range(1,10)]
    public float Frequency;

    private Rigidbody rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();

        StartCoroutine(SwingForce());
    }

    IEnumerator SwingForce()
    {
        rigidbody.AddForce(swingForce);

        yield return new WaitForSeconds(Frequency);


        StartCoroutine(SwingForce());

    }


}
