using UnityEngine;
using System.Collections.Generic;

public class EnviroumentManager : MonoBehaviour
{
    [HideInInspector] public string lastEnviroumentName;
    public List<GameObject> envirouments = new List<GameObject>();
}
