using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    private void Start()
    {
        Screen.SetResolution(1000, 400, false);
        Invoke("Load", 2);
    }

    private void Load()
    {
        SceneManager.LoadSceneAsync(1);
    }
}
