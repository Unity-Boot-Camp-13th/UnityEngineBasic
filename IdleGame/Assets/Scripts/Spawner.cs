using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Spawner : MonoBehaviour
{
    // 1. ������ ������ ������ �� �������ٴ� �� �������� �����Ǵ� ��찡 ����. (�� Ÿ��)

    // �� �۾��� ����Ƽ������ �ڷ�ƾ�̶�� ������� �����մϴ�.

    // �ڷ�ƾ�� ���� ���Ǵ� ���
    // 1. ���� ����
    // 2. ����, ��ų ��Ÿ��

    public int count;       // ������ ������ ����
    public float spawnTime; // ���� �ֱ� (�� Ÿ��, ���� Ÿ��..)
    public GameObject[] monster_prefab;
    public GameObject player;


    private void Start()
    {
        StartCoroutine(CSpawn());
        // StartCoroutine(�Լ���());
    }


    IEnumerator CSpawn()
    {
        // 1. ��� ������ ���ΰ�?
        Vector3 pos;
        

        // 2. �� �� ������ ���ΰ�?
        for (int i = 0; i < count; i++)
        {
            int index = Random.Range(0, monster_prefab.Length); // ���� ����
            // 3. � ���·� ������ ���ΰ�?
            pos = player.transform.position + Random.insideUnitSphere * 10.0f;
            // ���� ���� ���� (y ��ǥ = 0)
            pos.y = 0.0f;
            // Quaternion.idetity : ȸ�� �� 0
            // ���� ���¸� �״�� �����ϴ� ��쿡 ����ϴ� ��

            // ������ �Ÿ��� �������� ����� ����
            while (Vector3.Distance(pos, Vector3.zero) <= 3.0f)
            {
                pos = player.transform.position + Random.insideUnitSphere * 10.0f;
                pos.y = 0.0f;
            }

            Instantiate(monster_prefab[index], pos, Quaternion.identity);
        }
        yield return new WaitForSeconds(spawnTime);
        StartCoroutine(CSpawn());
        // yield return : ���� ���� �� �ٽ� ���ƿ��� �ڵ�
        // WaitForSeconds(float t) : �ۼ��� ����ŭ ����մϴ�. (�� ����)
    }
}