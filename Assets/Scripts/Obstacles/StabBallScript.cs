using UnityEngine;
using System.Collections;

public class StabBallScript : MonoBehaviour
{
    public float _rotateSpeed;
    public float _moveSpeed;
    public float _upDownSpeed;
    [HideInInspector]
    public float _trueSpeed;

    public bool _alone;

    private ObstacleInfo _obstacleInfo;

    void Awake()
    {
        _upDownSpeed *= Random.Range(0.5f, 1.8f);
        _trueSpeed = -_upDownSpeed;

        _obstacleInfo = transform.parent.GetComponent<ObstacleInfo>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, _rotateSpeed * Time.deltaTime));
        transform.position += new Vector3((_alone ? -GameController.GetInstance()._obsSpeed * Time.deltaTime : 0), _trueSpeed * Time.deltaTime, 0);

        if (transform.localPosition.y <= (-0.17f))
        {
            _trueSpeed = _upDownSpeed * (_obstacleInfo._isUpper ? 1f : -1f);
        }
        else if (transform.localPosition.y >= (4.28f))
        {
            _trueSpeed = -_upDownSpeed * (_obstacleInfo._isUpper ? 1f : -1f);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.tag == Tags.player)
        {
            collision.transform.GetComponent<PlayerController>().Dead();
        }
    }
}
