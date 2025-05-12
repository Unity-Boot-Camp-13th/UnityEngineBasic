using TMPro;
using UnityEngine;

/// <summary>
/// y축은 x 축 만들어진 것의 정가운데 오도록 만듦
/// </summary>
public class Cross : MonoBehaviour
{
    public GameObject prefab;
    public int width = 6;
    public int height = 6;
    public int widthCount = 2;
    public int heightCount = 2;
    public float gab = 1.5f;

    private void Start()
    {
        for (int i = 0; i < widthCount; i++)
        {
            for (int j = 0; j < width; j++)
            {
                var go = Instantiate(prefab, new Vector3(j * gab, i * gab), Quaternion.identity);
                go.transform.parent = transform;
            }
        }

        float i_start = width / 2 - heightCount / 2;
        float j_start = widthCount / 2 - height / 2;

        for (int i = 0; i < heightCount; i++)
        {
            for (int j = 0; j < height; j++)
            {
                var go = Instantiate(prefab, new Vector3((i_start + i) * gab, (j_start + j) * gab ), Quaternion.identity);
                go.transform.parent = transform;
            }
        }
    }

}
