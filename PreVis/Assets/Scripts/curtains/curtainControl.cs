using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class curtainAttributes
{
    public Vector3 startPosition { get; set; }
    public Vector3 Position;

    public Vector3 Rotation;

}

public class curtainControl : MonoBehaviour
{
    public curtainObject[] Curtains;


    public curtainScene[] curtainScenes;

    public int currentScene;

    [System.Serializable]
    public class curtainScene
    {
        public bool saveCurtain;

        public curtainAttributes[] curtainSetup;
    }




    // Start is called before the first frame update
    void Start()
    {
        Curtains = GetComponentsInChildren<curtainObject>();

   
    }





    // Update is called once per frame
    void Update()
    {
        
    }



    public void ApplySettings()
    {

        
        if(curtainScenes[currentScene].saveCurtain == true)
        {

            for (int i = 0; i < curtainScenes[currentScene].curtainSetup.Length; i++)
            {
                curtainScenes[currentScene].curtainSetup[i].Position = Curtains[i].transform.position - curtainScenes[currentScene].curtainSetup[i].startPosition;
                curtainScenes[currentScene].curtainSetup[i].Rotation = Curtains[i].transform.localRotation.eulerAngles;
            }


            curtainScenes[currentScene].saveCurtain = false;

        }



        


        for (int i=0; i < curtainScenes[currentScene].curtainSetup.Length; i++)
        {

            if (curtainScenes[currentScene].curtainSetup[i].startPosition == Vector3.zero)
            {
                curtainScenes[currentScene].curtainSetup[i].startPosition = Curtains[i].transform.position;
            }


            Curtains[i].transform.position = curtainScenes[currentScene].curtainSetup[i].Position + curtainScenes[currentScene].curtainSetup[i].startPosition;
            Curtains[i].transform.localRotation = Quaternion.Euler( curtainScenes[currentScene].curtainSetup[i].Rotation);
        }


    }


    private void OnValidate()
    {
        if (Curtains.Length == 0)
        { 
            Start();
        }

       ApplySettings();


    }

}
