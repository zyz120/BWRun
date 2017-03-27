using UnityEngine;
using System.Collections;

public class BlockSpin : MonoBehaviour {

    public float spinSpeed = 30.0f;
    public float moveSpeed = -0.5f;

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, spinSpeed * Time.deltaTime));
        transform.position += new Vector3(moveSpeed * Time.deltaTime, 0, 0);
    }

}
