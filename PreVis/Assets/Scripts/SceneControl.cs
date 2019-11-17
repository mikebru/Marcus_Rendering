using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneControl : MonoBehaviour
{

    public int currentScene;
    private int prevScene;

    public GameObject[] Cameras;

    public GameObject[] CurtainScenes;
    public GameObject[] FurnitureScenes;
    public GameObject[] LightingScenes;
    public GameObject[] MediaScenes;
    public GameObject[] CharacterScenes;

    public float transitionTime = 10;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnValidate()
    {

        SwitchScene();
    }

    void switchCam(int cur, int prev)
    {
        Cameras[prev].SetActive(false);

        Cameras[cur].SetActive(true);
    }


    void SwitchScene()
    {
        CharacterScenes[prevScene].SetActive(false);
        MediaScenes[prevScene].SetActive(false);
        CurtainScenes[prevScene].SetActive(false);
        FurnitureScenes[prevScene].SetActive(false);
        LightingScenes[prevScene].SetActive(false);


        CharacterScenes[currentScene].SetActive(true);
        MediaScenes[currentScene].SetActive(true);
        CurtainScenes[currentScene].SetActive(true);
        FurnitureScenes[currentScene].SetActive(true);
        LightingScenes[currentScene].SetActive(true);

        
        prevScene = currentScene;
    }

    public void SetScene(int index)
    {
        currentScene = index;

        CharacterScenes[prevScene].SetActive(false);

        StartCoroutine(SmoothTransitionCurtains(currentScene, prevScene));
        StartCoroutine(SmoothTransitionFurniture(currentScene, prevScene));
        StartCoroutine(SmoothTransitionLights(currentScene, prevScene));

        prevScene = currentScene;
    }


    IEnumerator SmoothTransitionFurniture(int cur, int prev)
    {

        FurnitureScenes[currentScene].SetActive(true);

        Renderer[] prevCurs = FurnitureScenes[prev].gameObject.GetComponentsInChildren<Renderer>();
        Renderer[] newCurs = FurnitureScenes[cur].gameObject.GetComponentsInChildren<Renderer>();


        Vector3[] newPos = new Vector3[newCurs.Length];
        Quaternion[] newRot = new Quaternion[newCurs.Length];

        FurnitureScenes[prevScene].SetActive(false);


        for (int i = 0; i < newCurs.Length; i++)
        {
            newPos[i] = newCurs[i].transform.position;
            newRot[i] = newCurs[i].transform.rotation;

        }

        float t = 0;

        while (t < transitionTime)
        {
            t += Time.deltaTime;

            float animation = Mathf.Sin((t / transitionTime) * Mathf.PI / 2);

            for (int i = 0; i < newCurs.Length; i++)
            {
                newCurs[i].transform.position = Vector3.Lerp(prevCurs[i].transform.position, newPos[i], animation);

                newCurs[i].transform.rotation = Quaternion.Lerp(prevCurs[i].transform.rotation , newRot[i], animation);

            }



            yield return null;

        }


    }

    IEnumerator SmoothTransitionCurtains(int cur, int prev)
    {

        CurtainScenes[currentScene].SetActive(true);

        curtainObject[] prevCurs = CurtainScenes[prev].gameObject.GetComponentsInChildren<curtainObject>();
        curtainObject[] newCurs = CurtainScenes[cur].gameObject.GetComponentsInChildren<curtainObject>();


        Vector3[] newPos = new Vector3[newCurs.Length];
        Quaternion[] newRot = new Quaternion[newCurs.Length];

        CurtainScenes[prevScene].SetActive(false);

        for (int i=0; i< newCurs.Length; i++)
        {
            newPos[i] = newCurs[i].transform.position;
            newRot[i] = newCurs[i].transform.rotation;

        }

        float t = 0;

        while(t < transitionTime)
        {
            t += Time.deltaTime;

            float animation = Mathf.Sin((t / transitionTime) * Mathf.PI/2);

            for (int i = 0; i < newCurs.Length; i++)
            {
                newCurs[i].transform.position = Vector3.Lerp(prevCurs[i].transform.position, newPos[i], animation);

                newCurs[i].transform.rotation = Quaternion.Lerp(prevCurs[i].transform.rotation, newRot[i], animation);

            }



            yield return null;

        }

        switchCharacters(cur, prev);
        switchMedia(cur, prev);
    }

    IEnumerator SmoothTransitionLights(int cur, int prev)
    {

        LightingScenes[currentScene].SetActive(true);

        Light[] prevCurs = LightingScenes[prev].gameObject.GetComponentsInChildren<Light>();
        Light[] newCurs = LightingScenes[cur].gameObject.GetComponentsInChildren<Light>();

        Color[] newColor = new Color[newCurs.Length];
        float[] range = new float[newCurs.Length];
        float[] spotAngle = new float[newCurs.Length];
        float[] intensity = new float[newCurs.Length];


        Vector3[] newPos = new Vector3[newCurs.Length];
        Quaternion[] newRot = new Quaternion[newCurs.Length];

        LightingScenes[prevScene].SetActive(false);

        for (int i = 0; i < newCurs.Length; i++)
        {
            newPos[i] = newCurs[i].transform.position;
            newRot[i] = newCurs[i].transform.rotation;
            newColor[i] = newCurs[i].color;
            range[i] = newCurs[i].range;
            spotAngle[i] = newCurs[i].spotAngle;
            intensity[i] = newCurs[i].intensity;

        }

        float t = 0;

        while (t < transitionTime)
        {
            t += Time.deltaTime;

            float animation = Mathf.Sin((t / transitionTime) * Mathf.PI / 2);

            for (int i = 0; i < newCurs.Length; i++)
            {
                newCurs[i].transform.position = Vector3.Lerp(prevCurs[i].transform.position, newPos[i], animation);

                newCurs[i].transform.rotation = Quaternion.Lerp(prevCurs[i].transform.rotation, newRot[i], animation);

                newCurs[i].color = Color.Lerp(prevCurs[i].color, newColor[i], animation);
                newCurs[i].range = Mathf.Lerp(prevCurs[i].range, range[i], animation);
                newCurs[i].spotAngle = Mathf.Lerp(prevCurs[i].spotAngle, spotAngle[i], animation);
                newCurs[i].intensity = Mathf.Lerp(prevCurs[i].intensity, intensity[i], animation);

            }



            yield return null;

        }


    }


    void switchCharacters(int cur, int prev)
    {
        CharacterScenes[prev].SetActive(false);

        CharacterScenes[cur].SetActive(true);

    }

    void switchMedia(int cur, int prev)
    {
        MediaScenes[prev].SetActive(false);

        MediaScenes[cur].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            SetScene(0);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SetScene(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            SetScene(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SetScene(3);
        }

        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            switchCam(0, 1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            switchCam(1, 0);
        }


    }
}
