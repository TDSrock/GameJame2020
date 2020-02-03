using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonController : MonoBehaviour
{

    [SerializeField]
    private GameObject presidentParent;


    [SerializeField]
    private bool keyDown;

    [SerializeField]
    private int maxIndex;

    public int numObjects = 5;
    public GameObject[] prefabs;
    public int index;


    private void Start() {
        float radius = 5f;
        numObjects = prefabs.Length;
        for (int i = 0; i < numObjects; i++) {
            float angle = i * Mathf.PI * 2f / numObjects;
            Vector3 newPos = new Vector3(Camera.main.transform.position.x + Mathf.Cos(angle) * radius, 0, Camera.main.transform.position.z + Mathf.Sin(angle) * radius);
            GameObject go = Instantiate(prefabs[i], newPos, Quaternion.FromToRotation(Vector3.forward, Camera.main.transform.position - newPos), presidentParent.transform);
            go.transform.position = new Vector3(go.transform.position.x, -2, go.transform.position.z);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Vertical") != 0 ) {
            if (!keyDown) {
                if (Input.GetAxisRaw("Vertical") < 0) {
                    if (index < maxIndex) {
                        index++;
                    } else {
                        index = 0;
                    }
                }
                else if (Input.GetAxisRaw("Vertical") > 0) {
                    if (index > 0) {
                        index--;
                    } else {
                        index = maxIndex;
                    }
                }
                keyDown = true;
            }
        } else {
            keyDown = false;
        }
    }


    public void StartGame() {
        //SceneManager.LoadScene("Timer");
    }

    public void QuitGame() {
        Application.Quit();
    }
}
