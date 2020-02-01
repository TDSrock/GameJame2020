using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour
{
    [SerializeField]
    MenuButtonController menuButtonController;

    [SerializeField]
    private int thisIndex;

    [SerializeField]
    private GameObject selector;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (menuButtonController.index == this.thisIndex) {
            selector.SetActive(true);
            if (Input.GetAxis("Submit") == 1) {
                gameObject.GetComponent<Button>().onClick.Invoke();
            }
        } else {
            selector.SetActive(false);
        }
    }
}
