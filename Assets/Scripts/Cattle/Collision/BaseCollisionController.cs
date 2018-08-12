using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCollisionController : MonoBehaviour
{
    public List<GameObject> collided = new List<GameObject>();

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collided.Add(collision.gameObject);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        collided.Remove(collision.gameObject);
    }
}
