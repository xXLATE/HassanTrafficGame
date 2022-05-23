using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Animator _animator;
    private Vector3 _destination;
    private LoseWindow _loseWindow;
    private bool _accident = false;
    private bool _turnLeft = false;
    private bool _turnRight = false;
    private GameManager _gameManager;

    private void Awake()
    {
        _loseWindow = FindObjectOfType<LoseWindow>();
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _animator.speed = Globals.movementSpeed;
        _gameManager = FindObjectOfType<GameManager>();
    }

    private void Update()
    {
        if (_turnLeft)
        {
            MoveToDestination();

            if (transform.position.x <= _destination.x)
                _turnLeft = false;

            if (transform.position.x <= -2.25f && _accident == false)
                Accident();
        }

        if (_turnRight)
        {
            MoveToDestination();

            if (transform.position.x >= _destination.x)
                _turnRight = false;

            if (transform.position.x >= 2.25f && _accident == false)
                Accident();
        }
    }

    private void MoveToDestination()
    {
        transform.position = Vector3.MoveTowards(transform.position, _destination, Globals.movementSpeed * Time.deltaTime);
    }

    public void TurnLeft()
    {
        if (!_turnRight && !_turnLeft)
        {
            _destination = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
            _turnLeft = true;
            _animator.SetTrigger("TurnLeft");
        }
    }

    public void TurnRight()
    {
        if (!_turnRight && !_turnLeft)
        {
            _destination = new Vector3(transform.position.x + 1, transform.position.y, transform.position.z);
            _turnRight = true;
            _animator.SetTrigger("TurnRight");
        }
    }

    private void Accident()
    {
        _accident = true;
        _gameManager.CheckHightScore();
        Time.timeScale = 0;

        _loseWindow.gameObject.SetActive(true);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            Accident();
    }
}
