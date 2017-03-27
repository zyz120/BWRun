using UnityEngine;
using System.Collections;

public class BlockEmitter : MonoBehaviour {

    public GameObject blockPrefab;
    public Transform parent;

    public float refleshTime;
    public float _spinSpeed;
    public float _moveSpeed;

    private float timer;
    

    void Awake()
    {
        timer = 0f;
        EmitBlock();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if(timer >= refleshTime)
        {
            EmitBlock();
        }
    }

    void EmitBlock()
    {
        GameObject block = Instantiate
            (blockPrefab, 
            transform.position + new Vector3(0, Random.Range(-2f, 2f), 0), 
            Quaternion.identity) 
            as  GameObject;
        block.transform.parent = parent;        

        float scale = Random.Range(0.6f, 1.4f);
        block.transform.localScale = new Vector3(scale, scale, scale);

        BlockSpin blockInfo = block.GetComponent<BlockSpin>();
        blockInfo.spinSpeed = _spinSpeed;
        blockInfo.moveSpeed = _moveSpeed;

        blockInfo.spinSpeed *= Random.Range(0.6f, 1.6f);
        blockInfo.moveSpeed *= Random.Range(0.7f, 1.3f);

        Destroy(block, 20f);
        timer = 0f;
        refleshTime *= Random.Range(0.9f, 1.11f);
        if (refleshTime <= 2f || refleshTime >= 4f)
            refleshTime = 3f;
    }

}
