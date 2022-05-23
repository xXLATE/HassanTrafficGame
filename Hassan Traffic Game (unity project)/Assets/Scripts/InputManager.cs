using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player _player;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x < _player.transform.position.x)
                _player.TurnLeft();
            else
                _player.TurnRight();
        }

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            _player.TurnLeft();

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            _player.TurnRight();
    }
}
