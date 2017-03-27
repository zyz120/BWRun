using UnityEngine;
using System.Collections;

public class StabScript : MonoBehaviour
{
    public float _moveSpeed;
    public bool _alone;

    void Awake()
    {
        Destroy(gameObject, 5f);
    }

    void Update()
    {
        transform.position += new Vector3((_alone ?( -GameController.GetInstance()._obsSpeed * Time.deltaTime) : 0), 0, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == Tags.player)
        {
            if (collision.transform.localScale.y * transform.localScale.y < 0 && _alone)
                return;

            collision.GetComponent<PlayerController>().Dead();
        }
    }



}
