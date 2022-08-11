using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class InitSceneManager : MonoBehaviour
{
    //[SerializeField] private string m_persistentScene;
    [SerializeField] private AssetReference m_tutorial;
    [SerializeField] private AssetReference m_persistentLevel;
    [SerializeField] private AssetReference m_persistentManagers;

    private AsyncOperationHandle persistantSceneHandle;


    public void Awake()
    {
        LoadPersistenScene();
    }
    public async void LoadPersistenScene()
    {
        /*  SceneManager.LoadSceneAsync(m_persistentScene).completed += (handle) =>
          {
              SceneManager.LoadScene(m_tutorial, LoadSceneMode.Additive);
              SceneManager.LoadSceneAsync(m_persistentLevel, LoadSceneMode.Additive);
              //SceneManager.SetActiveScene()


          };*/
        persistantSceneHandle = m_persistentManagers.LoadSceneAsync(LoadSceneMode.Single, true);
        await persistantSceneHandle.Task;
        m_persistentLevel.LoadSceneAsync(LoadSceneMode.Additive, true);
    }
}
