using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ceil : MonoBehaviour
{
    public GameObject m_Prefab;
    public int m_PoolSize = 250;
    public int m_InstancesPerTile = 10;
    public float m_Height;

    public int m_BaseHash = 347652783;
    public float m_Size = 100.0f;

    List<Transform> m_Instances = new List<Transform>();
    int m_Used;
    int m_LocX, m_LocZ;

    void Awake()
    {
        for (int i = 0; i < m_PoolSize; ++i)
        {
            var go = Instantiate(m_Prefab, Vector3.zero, Quaternion.identity) as GameObject;
            go.SetActive(false);
            m_Instances.Add(go.transform);
        }
    }

    void OnEnable()
    {
        m_LocX = ~0;
        m_LocZ = ~0;
        UpdateInstances();
    }

    void OnDestroy()
    {
        for (int i = 0; i < m_Instances.Count; ++i)
        {
            if (m_Instances[i])
                Destroy(m_Instances[i].gameObject);
        }
        m_Instances.Clear();
    }

    void Update()
    {
        if(this.transform.position.y > 2.0f && Mathf.Abs(Vector3.Dot(this.transform.forward.normalized,Vector3.right)) < 0.3)
        {
            UpdateInstances();
        }
    }

    void UpdateInstances()
    {
        var x = (int)Mathf.Floor(transform.position.x / m_Size);
        var z = (int)Mathf.Floor(transform.position.z / m_Size);
        if (x == m_LocX && z == m_LocZ)
            return;

        m_LocX = x;
        m_LocZ = z;

        m_Used = 0;
        for (var i = x - 2; i <= x + 2; ++i)
        {
            for (var j = z - 2; j <= z + 2; ++j)
            {
                var count = UpdateTileInstances(i, j);
                if (count != m_InstancesPerTile)
                    return;
            }
        }

        // Deactivate the remaining active elements in the pool.
        // Here we assume all active elements are contiguous and first in the list.
        for (int i = m_Used; i < m_PoolSize && m_Instances[i].gameObject.activeSelf; ++i)
            m_Instances[i].gameObject.SetActive(false);
    }

    int UpdateTileInstances(int i, int j)
    {
        var count = System.Math.Min(m_InstancesPerTile, m_PoolSize - m_Used);
        for (var end = m_Used + count; m_Used < end; ++m_Used)
        {
            float x = 0;
            float y = 0;

            var pos = new Vector3((i + x) * m_Size, m_Height, (j + y) * m_Size);

            m_Instances[m_Used].position = pos;
            m_Instances[m_Used].gameObject.SetActive(true);
        }

        if (count < m_InstancesPerTile)
            Debug.LogWarning("Pool exhausted", this);

        return count;
    }

}
