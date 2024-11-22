using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnemyManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject[] enemyPool;
    public int poolSize = 10;
    public float curTime;
    public float spawnTime = 2f;
    int spawnCnt = 1;
    public int maxSpawnCnt = 10;
    public PlayerState playerState;

    void Start()
    {
        playerState = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerState>();
        //enemyPool = new GameObject[poolSize];
        //for (int i = 0; i < poolSize; ++i)
        //{
        //    enemyPool[i] = Instantiate(enemyPrefab) as GameObject;
        //    enemyPool[i].SetActive(false);
        //}
    }

    void Update()
    {
        if (playerState.isDead)
            return;

        if (spawnCnt > maxSpawnCnt)
            return;          

        curTime += Time.deltaTime;

        if (curTime > spawnTime)
        {
            // Instantiate() 생성
            curTime = 0;
            float x = Random.Range(-10f, 10f);
            GameObject enemy = Instantiate(enemyPrefab, new Vector3(x, 0, this.transform.position.z), Quaternion.identity);
            // 골때림, -> 임마 생성 후 포지션 값 변경하면 안 먹힘. NavMesh Agent 이슈로 예상 됨. 

            //enemy.transform.position = new Vector3(x, 0, 20f);
            //Debug.Log(enemy.transform.position);
            enemy.name = "ENEMY_" + spawnCnt;
            ++spawnCnt;

            // 오브젝트 풀링
            //for (int i = 0; i < enemyPool.Length; i++)
            //{
            //    if (enemyPool[i].activeSelf == true)
            //        continue;
            //    float x = Random.Range(-20f, 20f);
            //    enemyPool[i].transform.position = new Vector3(x, 1, 20f);
            //    enemyPool[i].SetActive(true);
            //    enemyPool[i].name = "ENEMY_" + spawnCnt;
            //    ++spawnCnt;
            //    break;
            //}
        }
    }
}