using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float _jumpForce;
    public GameObject _bloodPrefab;

    [HideInInspector]
    public int _jumpTimes;
    [HideInInspector]
    public bool _needChange;
    [HideInInspector]
    public Vector2 _velocity;
    [HideInInspector]
    public bool _isUpper;

    private Rigidbody2D rigidbody;
    private Animator anim;

    private bool _lastRayResult;

    void Awake()
    {
        _jumpTimes = 0;
        _lastRayResult = true;
        _isUpper = true;
        _needChange = false;
        rigidbody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Jump();
        }

        RayCastTry();

        VelocityLimit();
    }

    void Jump()
    {
        if (_jumpTimes < 2)
        {
            rigidbody.velocity = new Vector2(0, 0);
            rigidbody.AddForce(new Vector2(0, transform.localScale.y > 0 ? _jumpForce : -_jumpForce));
            _jumpTimes++;
        }
    }

    /*
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag != Tags.horizon && collision.transform.tag != Tags.ground)
            return;
        
        if (collision.transform.tag == Tags.horizon)
        {
            if(_needChange)
            {
                ChangeWorld();
                return;
            }
        }
        
        _jumpTimes = 0;
        anim.SetBool("Jump", false);
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.transform.tag == Tags.ground || collision.transform.tag ==Tags.horizon)
        {
            _needChange = true;
            anim.SetBool("Jump", true);
        }
    }
    */

    public void Dead()
    {
        GameController.GetInstance()._playerDead = true;
        GameObject go = Instantiate(_bloodPrefab, transform.position, Quaternion.identity) as GameObject;
        Destroy(go, 2f);
        Destroy(gameObject);
    }

    void ChangeWorld()
    {
        GameController.GetInstance().ChangeWorld();
        _needChange = false;
        _isUpper = !_isUpper;

        transform.position += new Vector3(0f, _isUpper ? 0.5f : -0.5f, 0f);
        rigidbody.gravityScale *= -1;
        transform.localScale = new Vector3(transform.localScale.x, -transform.localScale.y, transform.localScale.z);

        rigidbody.velocity = _velocity;

        // 处理BUG用到
        Invoke("SetNeedChangeToFalse", 0.1f);

    }

    void VelocityLimit()
    {
        if (rigidbody.velocity.y > 25)
        {
            rigidbody.velocity = new Vector2(0, 25);
        }
        else if (rigidbody.velocity.y < -25)
        {
            rigidbody.velocity = new Vector2(0, -25);
        }
    }

    void RayCastTry()
    {
        bool temp = false;

        RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, new Vector2(0, transform.localScale.y > 0 ? -1 : 1), 0.6f, (1 << 10));

        if (hitInfo)
        {
            _jumpTimes = 0;
            anim.SetBool("Jump", false);
            temp = true;

            if (_lastRayResult == false && _needChange && hitInfo.collider.gameObject.tag == Tags.horizon)
            {
                ChangeWorld();
                _needChange = false;
            }

        }
        else
        {
            anim.SetBool("Jump", true);
            temp = false;

            if (_lastRayResult == true)
            {
                _needChange = true;
                _jumpTimes++;
            }
        }

        _lastRayResult = temp;
    }

    void SetNeedChangeToFalse()
    {
        _needChange = false;
    }

}
