using UnityEngine;
using System.Collections;

public class EdgeTrigger : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.player)
        {
            collision.GetComponent<PlayerController>().Dead();
        }
    }
}
