using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class makeEnemy : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject monsterPrefab; // ¹ÖÎïÔ¤ÖÆÌå
    void Start()
    {
        SpawnMonster();
    }

    // Update is called once per frame
    float time = 0;
    void Update()
    {
        time += Time.deltaTime;
        if(time >= 2)
        {
            time = 0;
            SpawnMonster();
        }
    }
    void SpawnMonster()
    {
        Vector3 randomPosition = new Vector3(
            Random.Range(144f, 155f),
            25.2f,
            Random.Range(192, 215f)
        );

        Instantiate(monsterPrefab, randomPosition, Quaternion.identity);
    }
}
