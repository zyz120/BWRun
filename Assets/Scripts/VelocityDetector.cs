using UnityEngine;
using System.Collections;

public class VelocityDetector : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.player)
        {
            collision.GetComponent<PlayerController>()._velocity = collision.GetComponent<Rigidbody2D>().velocity;
        }
    }
}
