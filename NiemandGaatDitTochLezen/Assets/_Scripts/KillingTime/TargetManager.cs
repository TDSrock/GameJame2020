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
    private Animator selectedAnimator;

    private bool isRotating;
    private float rotationTarget;

    public int numObjects = 5;
    public GameObject[] prefabs;

    private bool killed;



  

    void Start() {
        float radius = 5f;

        for (int i = 0; i < numObjects; i++) {
            float angle = i * Mathf.PI * 2f / numObjects;
            Vector3 newPos = new Vector3(Camera.main.transform.position.x + Mathf.Cos(angle) * radius, 0, Camera.main.transform.position.z + Mathf.Sin(angle) * radius);
            GameObject go = Instantiate(prefabs[i], newPos, Quaternion.FromToRotation(Vector3.forward, Camera.main.transform.position - newPos), presidentParent.transform);
            go.transform.position = new Vector3(go.transform.position.x, -2, go.transform.position.z);
        }
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.A)) {
            
            if (currentIndex >= numObjects - 1) {
                currentIndex = 0;
                            
            } else {
                currentIndex += 1;
            }
            nameTarget.text = prefabs[currentIndex].name;
            rotationTarget += (360 / numObjects);
            selectedAnimator = prefabs[currentIndex].GetComponent<Animator>();

        }

        if (Input.GetKeyDown(KeyCode.D)) {

            if (currentIndex <= 0) {
                currentIndex = numObjects - 1;
            }
            else {
                currentIndex -= 1;
            }
            nameTarget.text = prefabs[currentIndex].name;
            rotationTarget -= (360 / numObjects);
            selectedAnimator = prefabs[currentIndex].GetComponent<Animator>();
        }

        if (Input.GetMouseButtonDown(0)) {
            Debug.Log(prefabs[currentIndex].name + " is DEAD");


           prefabs[currentIndex].GetComponent<Animator>();

            // KILL TARGET
        }

        Quaternion newRotation = Quaternion.Euler(0, rotationTarget, 0);
        presidentParent.transform.rotation = Quaternion.Lerp(presidentParent.transform.rotation, newRotation, Time.deltaTime * 2);

    }
}
