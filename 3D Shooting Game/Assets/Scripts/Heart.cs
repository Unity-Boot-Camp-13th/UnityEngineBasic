using UnityEngine;

public class Heart : MonoBehaviour
{
    public GameObject prefab;

    public float t;
    public float blockCount = 40;

    void Start()
    {
        for (t = 0; t < blockCount; t++)
        {
            float x = 16 * Mathf.Sin(t) * Mathf.Sin(t) * Mathf.Sin(t);
            float y = 13 * Mathf.Cos(t) - 5 * Mathf.Cos(2 * t) - 2 * Mathf.Cos(3 * t) - Mathf.Cos(4 * t);


            Instantiate(prefab, new Vector3(x, y), Quaternion.identity);
        }

    }
}
