using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ShaderChange : MonoBehaviour
{
    public bool _shouldSlideOut;
    public bool _shouldSlideIn;

    public bool _isUpperShader;

    public float _scaleSpeed;

    public float _rotationWhenIn;
    public float _rotationWhenOut;
    public float _positionYWhenIn;
    public float _positionYWhenOut;

    public Text _scoreText;

    void Update()
    {
        if(_shouldSlideIn)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _rotationWhenIn));
            transform.position = new Vector3(0, _positionYWhenIn, 0);
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, _scaleSpeed * Time.deltaTime);
            if (_isUpperShader)
            {
                _scoreText.color = Color.Lerp(_scoreText.color, Color.white, _scaleSpeed * Time.deltaTime);
            }
        }
        if(_shouldSlideOut)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, _rotationWhenOut));
            transform.position = new Vector3(0, _positionYWhenOut, 0);
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1f, 0f, 1f), _scaleSpeed* Time.deltaTime);
            if (_isUpperShader)
            {
                _scoreText.color = Color.Lerp(_scoreText.color, Color.black, _scaleSpeed * Time.deltaTime);
            }
        }
    }



}
