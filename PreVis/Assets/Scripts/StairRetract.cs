using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairRetract : MonoBehaviour
{
    [Range(0,120)]
    public float rotation;

    public bool MoveTogether;
    public bool Animate;

    public StairRetract otherStair;

    // Start is called before the first frame update
    void Start()
    {
        if(Animate == true)
        {
            StartCoroutine(delayStairs());
        }
    }


    IEnumerator delayStairs()
    {

        float t = 0;

        yield return new WaitForSeconds(3);

        while(t < 10)
        {
            rotation = Mathf.Lerp(120, 0, t / 10.0f);
            MoveStairs();
            otherStair.SetRotation(rotation);
            otherStair.MoveStairs();

            t += Time.deltaTime;
            yield return null;
        }

       yield return new WaitForSeconds(15);

        t = 0;

        while (t < 10)
        {
            rotation = Mathf.Lerp(0 ,120, t / 10.0f);

            MoveStairs();
            otherStair.SetRotation(rotation);
            otherStair.MoveStairs();

            t += Time.deltaTime;
            yield return null;
        }

    }

    private void OnValidate()
    {
        if(MoveTogether == true)
        {
            otherStair.SetRotation(rotation);
            otherStair.MoveStairs();
        }

        MoveStairs();
    }

    public void SetRotation(float newRot)
    {
        rotation = newRot;
    }


    void MoveStairs()
    {
        //Debug.Log(rotation);

        for (int i=0; i< transform.childCount; i++)
        {

            transform.GetChild(i).localRotation = Quaternion.Euler(new Vector3(0, (rotation)/((i * 1.0f/ transform.childCount) +1),0));
        }


    }

}
