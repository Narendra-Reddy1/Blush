using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InitSceneManager : MonoBehaviour
{
    [SerializeField] private string m_persistentScene;
    [SerializeField] private string m_tutorial;
    [SerializeField] private string m_persistentLevel;

    public void Awake()
    {
        LoadPersistenScene();
    }
    public void LoadPersistenScene()
    {
        SceneManager.LoadSceneAsync(m_persistentScene).completed += (handle) =>
        {
            SceneManager.LoadScene(m_tutorial, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(m_persistentLevel, LoadSceneMode.Additive);
            //SceneManager.SetActiveScene()


        };
    }
}
