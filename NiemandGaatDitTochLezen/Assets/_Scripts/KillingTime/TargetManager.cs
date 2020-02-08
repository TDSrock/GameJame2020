using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TargetManager : MonoBehaviour {
    [SerializeField]
    private int currentIndex;

    [SerializeField]
    private GameObject presidentParent;

    [SerializeField]
    private TextMeshProUGUI nameTarget;
    [SerializeField]
    private TextMeshProUGUI correctText;
    [SerializeField]
    private TextMeshProUGUI wrongText;

    [SerializeField]
    public Animator selectedAnimator;

    private bool isRotating;
    private float rotationTarget;

    public GameObject[] prefabs;
    public GameObject[] instances;

    private bool killed;



  

    void Start() {
        killed = false;
        instances = new GameObject[prefabs.Length];
        float radius = 5f;
        for (int i = 0; i < prefabs.Length; i++) {
            float angle = i * Mathf.PI * 2f / prefabs.Length;
            Vector3 newPos = new Vector3(Camera.main.transform.position.x + Mathf.Cos(angle) * radius, 0, Camera.main.transform.position.z + Mathf.Sin(angle) * radius);
            GameObject go = Instantiate(prefabs[i], newPos, Quaternion.FromToRotation(Vector3.forward, Camera.main.transform.position - newPos), presidentParent.transform);
            instances[i] = go;
            go.transform.position = new Vector3(go.transform.position.x, -2, go.transform.position.z);
        }
        selectedAnimator = instances[1].GetComponent<Animator>();
    }

    private void Update() {
        if (!killed)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {

                if (currentIndex >= prefabs.Length - 1)
                {
                    currentIndex = 0;

                }
                else
                {
                    currentIndex += 1;
                }
                nameTarget.text = prefabs[currentIndex].name;
                rotationTarget += (360 / prefabs.Length);
                selectedAnimator = instances[currentIndex].GetComponent<Animator>();

            }

            if (Input.GetKeyDown(KeyCode.D))
            {

                if (currentIndex <= 0)
                {
                    currentIndex = prefabs.Length - 1;
                }
                else
                {
                    currentIndex -= 1;
                }
                nameTarget.text = prefabs[currentIndex].name;
                rotationTarget -= (360 / prefabs.Length);
                selectedAnimator = instances[currentIndex].GetComponent<Animator>();
            }

            if (Input.GetMouseButtonDown(0))
            {
                killed = true;
                Debug.Log(prefabs[currentIndex].name + " is DEAD");

                if (selectedAnimator.gameObject.activeSelf)
                {

                    selectedAnimator.SetBool("isDead", true);
                }
                CheckAnswer();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0))
            {
                Application.Quit();
            }
        }
       

        Quaternion newRotation = Quaternion.Euler(0, rotationTarget, 0);
        presidentParent.transform.rotation = Quaternion.Lerp(presidentParent.transform.rotation, newRotation, Time.deltaTime * 2);

      
    }

    public void CheckAnswer() {
        if (PersisntSceneManagementComponent.instance)
        {
            if(instances[currentIndex].GetComponent<CheckClean>().president == PersisntSceneManagementComponent.instance.presidentToFire)
            {
                correctText.gameObject.SetActive(true);
            }
            else
            {
                wrongText.gameObject.SetActive(true);
            }
        }
        else
        {
            if (instances[currentIndex].GetComponent<CheckClean>().president.isClean)
            {
                correctText.gameObject.SetActive(true);
            }
            else
            {
                wrongText.gameObject.SetActive(true);
            }
        }

    }
}
