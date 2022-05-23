using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour
{
    private List<int> _busyLanes = new List<int>();
    [SerializeField] private List<GameObject> _enemiesPrefabs = new List<GameObject>();

    private void Start()
    {
        StartCoroutine(WaitForStart());
    }

    private IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(1);

        for (int i = 0; i < Globals.carsCount; i++)
        {
            SpawnEnemy();

            yield return new WaitForSeconds(1);
        }
    }

    private void SpawnEnemy()
    {
        var newEnemy = Instantiate(_enemiesPrefabs[Random.Range(0, _enemiesPrefabs.Count)]);
        newEnemy.transform.parent = gameObject.transform;

        var newEnemyComp = newEnemy.GetComponent<Enemy>();
        newEnemyComp.faster = System.Convert.ToBoolean(Random.Range(0, 2));

        while (true)
        {
            var laneNumber = Random.Range(-2, 3);

            if (!_busyLanes.Contains(laneNumber))
            {
                _busyLanes.Add(laneNumber);
                break;
            }
        }

        if (newEnemyComp.faster)
            newEnemy.transform.position = new Vector3(_busyLanes[^1], -4f, transform.position.z);
        else
            newEnemy.transform.position = new Vector3(_busyLanes[^1], 4f, transform.position.z);
    }

    public void DestroyEnemy(Enemy enemy)
    {
        _busyLanes.Remove(System.Convert.ToInt32(enemy.transform.position.x));
        Destroy(enemy.gameObject);

        SpawnEnemy();
    }
}
