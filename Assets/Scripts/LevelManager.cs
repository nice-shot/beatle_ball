﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Canvas panel;
    private bool start = false;
    private bool instructionalArrows = false;
    private bool instructionalArrowsCoroutine = false;
    private bool instructionalSpace = false;
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
                panel.GetComponent<Animator>().SetTrigger("start");
            }
        }
        else if (!instructionalSpace)
        {
            if (!instructionalArrows)
            {
                if (!instructionalArrowsCoroutine) StartCoroutine("ShowInstructionalArrows");
                instructionalArrowsCoroutine = true;
            }
            else
            {
                if (!Mathf.Approximately(Input.GetAxis("Horizontal"), 0))
                {
                    panel.GetComponent<Animator>().SetTrigger("stopInstructionalArrows");
                }
            }
        }
        else if (instructionalSpace && instructionalArrows)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                panel.GetComponent<Animator>().SetTrigger("stopInstructionalSpace");
            }
            panel.transform.GetChild(7).gameObject.SetActive(true);
            panel.GetComponent<Animator>().SetTrigger("instructionalSpace");
        }
    }

    public IEnumerator ShowInstructionalArrows()
    {
        yield return new WaitForSeconds(2);
        panel.transform.GetChild(5).gameObject.SetActive(true);
        panel.transform.GetChild(6).gameObject.SetActive(true);
        panel.GetComponent<Animator>().SetTrigger("instructionalArrows");
        instructionalArrows = true;
    }

    public void ShowInstructionalSpace()
    {
        instructionalSpace = true;
    }
}
