using System.Collections;
using System.Collections.Generic;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadNextScene : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }
}
