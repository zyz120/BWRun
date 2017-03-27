using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour
{
    public float _moveSpeed;
    public bool _alone;

    void Awake()
    {
        Destroy(gameObject, 5f);
    }

    void Update ()
    {
        transform.position += new Vector3(_alone ? (-GameController.GetInstance()._obsSpeed * Time.deltaTime ): 0, 0, 0);
    }
}
