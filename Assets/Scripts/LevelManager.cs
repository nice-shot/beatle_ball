using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Canvas startPanel;
    private bool start = false;
    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!start)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                Time.timeScale = 1;
                start = true;
                Camera.main.GetComponent<FocusCamera>().start = true;
                startPanel.enabled = false;
            }
        }
    }
}
