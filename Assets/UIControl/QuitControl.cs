using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitControl : MonoBehaviour
{
    // Start is called before the first frame

    //�����첽�������� 
    AsyncOperation operation;

    public void Cloth()
    {
        StartCoroutine(loadScene());
    }

   IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync("Menu");
        yield return operation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
