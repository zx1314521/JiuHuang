using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    // Start is called before the first frame

    //申请异步操作的类 
    AsyncOperation operation;

    public void OnClickStart()
    {
        //开始一个协程
        StartCoroutine(loadScene());
    }

    IEnumerator loadScene()
    {
        operation = SceneManager.LoadSceneAsync("PlayGame");
        yield return operation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
