using UnityEngine;
using System.Collections.Generic;

public class Enviroument : MovableObject
{
    private EnviroumentManager _enviroumentManager;

    private void Awake()
    {
        _enviroumentManager = GetComponentInParent<EnviroumentManager>();
        WhenMoving += MoveToBeginning;
    }

    private void Start()
    {
        SpawnEnviroument();
    }

    private void MoveToBeginning()
    {
        Destroy(transform.GetChild(0).gameObject);

        SpawnEnviroument();
    }

    private void SpawnEnviroument()
    {
        var newObj = _enviroumentManager.envirouments[Random.Range(0, _enviroumentManager.envirouments.Count)];

        while (true)
        {
            if (newObj.name == _enviroumentManager.lastEnviroumentName)
                newObj = _enviroumentManager.envirouments[Random.Range(0, _enviroumentManager.envirouments.Count)];
            else
                break;
        }

        var newEnv = Instantiate(newObj);
        newEnv.name = newEnv.name.Split(')')[0] + ")";
        newEnv.transform.SetParent(transform);
        newEnv.transform.localPosition = new Vector3(0, 0, 0);

        _enviroumentManager.lastEnviroumentName = newEnv.name;
    }
}
