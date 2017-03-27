using UnityEngine;
using System.Collections;

public class BackgroundChange : MonoBehaviour
{
    private SpriteRenderer renderer;

    public bool _changeToOne;
    public bool _changeToTwo;
    public bool _changeToThree;

    public float _changeSpeed;
    

    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();
        _changeToOne = _changeToTwo = _changeToThree = false;
        _changeSpeed = 3f;
    } 

   	void Update ()
    {
        if (_changeToOne)
        {           
            renderer.color = Color.Lerp(renderer.color, Color.white, _changeSpeed * Time.deltaTime);
        }
        else if (_changeToTwo)
        {
            renderer.color = Color.Lerp(renderer.color, Color.yellow, _changeSpeed * Time.deltaTime);
        }
        else if (_changeToThree)
        {
            renderer.color = Color.Lerp(renderer.color, Color.red, _changeSpeed * Time.deltaTime);
        }

    }
}
