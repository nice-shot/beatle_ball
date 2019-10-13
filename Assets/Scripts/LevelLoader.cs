using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelLoader : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("RestartLevel");
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(5);
        SceneManager.LoadScene("PrototypeLevel");
    }
}
