using UnityEngine;
using System.Collections;

public class WallTrigger : MonoBehaviour
{
    private ObstacleInfo _obstacleInfo;

    void Awake()
    {
        _obstacleInfo = transform.parent.GetComponent<ObstacleInfo>();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == Tags.player)
        {
            if (_obstacleInfo._isUpper != collision.GetComponent<PlayerController>()._isUpper)
            {
                collision.GetComponent<PlayerController>()._needChange = false;
            }
        }
    }
}
