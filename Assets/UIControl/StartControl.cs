using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartControl : MonoBehaviour
{
    // Start is called before the first frame

    //�����첽�������� 
    AsyncOperation operation;

    public void OnClickStart()
    {
        //��ʼһ��Э��
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
