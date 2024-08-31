using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    public Transform player; // ָ����ҵ�Transform
    public float smoothing = 5f; // ƽ��������ٶ�

    private Vector3 offset;

    void Start()
    {
        // ������������֮���ƫ����
        offset = transform.position - player.position;
    }

    void Update()
    {
        // ���������λ��Ϊ��ҵĵ�ǰλ�ü���ƫ����
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing * Time.deltaTime);
    }
}

