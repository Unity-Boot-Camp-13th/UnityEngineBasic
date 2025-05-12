using UnityEngine;

public class Smile : MonoBehaviour
{
    public GameObject prefab;
    public int count = 30;
    public float radius = 10f;

    private void Start()
    {
        // <얼굴>
        // 카운트만큼 실행
        for (int i = 0; i < count; i++)
        {
            float radian = i * Mathf.PI * 2 / count;
            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float degree = -radian * Mathf.Rad2Deg; // 오브젝트가 중앙을 바라보도록

            Quaternion rotation = Quaternion.Euler(0, degree, 0);
            var go = Instantiate(prefab, pos, rotation);
            go.transform.parent = transform;
        }

        // <눈>
        for (int i = 0; i < 3; i++)
        {var go1= Instantiate(prefab, new Vector3(radius / 2, 0, - (i + 2)), Quaternion.identity);
            var go2 = Instantiate(prefab, new Vector3(- radius / 2, 0, - (i + 2)), Quaternion.identity);
            go1.transform.parent = transform;
            go2.transform.parent = transform;
        }

        // <입>
        for (int i = 0; i < 5; i++)
        {
            float radian = i * Mathf.PI * 2 / count;
            float x = Mathf.Cos(radian) * radius;
            float z = Mathf.Sin(radian) * radius;
            Vector3 pos = transform.position + new Vector3(x, 0, z);
            float degree = -radian * Mathf.Rad2Deg; // 오브젝트가 중앙을 바라보도록

            Instantiate(prefab, new Vector3(z/2, 0, x/2), Quaternion.identity);
            Instantiate(prefab, new Vector3(- z / 2, 0, x / 2), Quaternion.identity);
        }
    }
}
