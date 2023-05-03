using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField]
    PoolingListSO _poolingListSO;

    [SerializeField]
    private Transform _spawnPointParent;

    [SerializeField]
    private Transform _playerTrm;
    public Transform PlayTrm => _playerTrm;

    [SerializeField]
    private SpawnListSO _spawnList;
    private float[] _spawnWeight;

    private List<Transform> _spawnPointList = new List<Transform>();

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.LogError("Multiple GameManager is running! Check!");
        }
        Instance = this;

        TimeController.Instance = gameObject.AddComponent<TimeController>();

        MakePool();

        _spawnPointParent.GetComponentsInChildren<Transform>(_spawnPointList);
        _spawnPointList.RemoveAt(0);


        _spawnWeight = _spawnList.SpawnPairs.Select(s => s.spawnPercent).ToArray();
    }

    private void MakePool()
    {
        PoolManager.Instance = new PoolManager(transform);

        _poolingListSO.list.ForEach(p => PoolManager.Instance.CreatePool(p.prefab, p.poolCount)); //리스트에 있는 모든
    }

    IEnumerator Start()
    {
        float currentTime = 0;
        while (true)
        {
            currentTime += Time.deltaTime; //시퀀스를 줘서 4~6마리가 스폰포인트 반경 2유닛 정도의 범위에서 순차적으로 생성
            if (currentTime >= 5f)
            {
                currentTime = 0;
                int idx = UnityEngine.Random.Range(0, _spawnPointList.Count);


                int cnt = Random.Range(3, 7);
                for (int i = 0; i < cnt; i++)
                {
                    int sIndex = GetRandomSpawnIndex();

                    EnemyBrain enemy = PoolManager.Instance.Pop(_spawnList.SpawnPairs[sIndex].prefab.name) as EnemyBrain;
                    
                    Vector2 positionOffset = Random.insideUnitCircle * 2;

                    enemy.transform.position = _spawnPointList[idx].position + (Vector3)positionOffset;
                    enemy.ShowEnemy();

                    float showTime = Random.Range(0.1f, 0.3f);
                    yield return new WaitForSeconds(showTime);
                }
            }
            yield return null;
        }
    }

    private int GetRandomSpawnIndex()
    {
        float sum = 0;
        for (int i = 0; i < _spawnWeight.Length; i++)
        {
            sum += _spawnWeight[i];
        }

        float randomValue = Random.Range(0f, sum);
        float tempSum = 0;

        for(int i = 0; i < _spawnWeight.Length;i++)
        {
            if(randomValue >= tempSum && randomValue < tempSum + _spawnWeight[i])
            {
                return i;
            }
            else
            {
                tempSum += _spawnWeight[i];
            }
        }
        return 0;
    }

}
