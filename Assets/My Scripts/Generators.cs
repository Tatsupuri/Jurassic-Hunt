using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generators : MonoBehaviour
{
    [SerializeField] private float size = 0.1f;//針部分のタイルのスケール
    [SerializeField] private float maxSize = 8;//全長

    [SerializeField] private float hight = 2.0f;

    public GameObject prefab;
    public Transform parent;
    // Start is called before the first frame update
    void Start()
    {
        float initial = - maxSize/2.0f + size * 5f/2.0f; //スケール*5fが実際の大きさ..?
        int num = Mathf.CeilToInt(maxSize/5f / size);
        Vector3 genPos = new Vector3();

        for(int i = 0; i < num; i++)
        {
            for(int j = 0; j < num ; j++)
            {
                genPos.x = initial + i * size * 5f;
                genPos.y = hight;
                genPos.z = initial + j * size * 5f;
                GameObject generator = Instantiate(prefab, genPos , Quaternion.Euler(0,0,0));
                generator.transform.SetParent(parent);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
