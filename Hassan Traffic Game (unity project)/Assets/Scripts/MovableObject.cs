using System;
using UnityEngine;

public class MovableObject : MonoBehaviour
{
    public event Action WhenMoving;

    private void Update()
    {
        transform.Translate(Globals.movementSpeed * Time.deltaTime * Vector3.down);

        if (transform.position.y <= -5.6f)
        {
            MoveToBeginning();
        }
    }

    private void MoveToBeginning()
    {
        transform.position = new Vector3(transform.position.x, 5.6f, transform.position.z);
        try
        {
            WhenMoving.Invoke();
        }
        catch { }
    }
}
