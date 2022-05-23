using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float _speed;
    private EnemyManager _manager;

    [HideInInspector] public bool faster;

    private void Start()
    {
        _manager = GetComponentInParent<EnemyManager>();
        _speed = Globals.movementSpeed - Random.Range(0, Globals.movementSpeed - 0.2f);
    }

    private void Update()
    {
        if (faster)
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.right);
            
            if (transform.position.y >= 4)
                _manager.DestroyEnemy(this);
        }
        else
        {
            transform.Translate(_speed * Time.deltaTime * Vector3.left);

            if (transform.position.y <= -4)
                _manager.DestroyEnemy(this);
        }
    }
}
