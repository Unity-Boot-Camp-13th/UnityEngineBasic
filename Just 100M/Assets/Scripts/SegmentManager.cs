using System.Collections.Generic;
using UnityEngine;

public class SegmentManager : MonoBehaviour
{
    // 몬스터가 생성될 빈 게임오브젝트 위치
    
    public Transform monsterSpwanPoints;

    public GameObject[] segmentPrefabs; // 등록할 세그먼트
    private List<GameObject> segments; // 세그먼트 리스트

    private Transform player_transform; // 플레이어 위치
    private float spawnZ = 0.0f;        // 스폰 (z축)
    private float segmentLength = 9.0f; // 세그먼트의 길이
    private float passZone = 20.0f;     // 세그먼트 유지 거리
    private int segment_on_screen = 5;  // 화면에 배치할 세그먼트 개수

    private void Start()
    {
        // 세그먼트 리스트 생성
        segments = new List<GameObject>();

        // 게임 씬에서 태그 검색해서 트랜스폼 적용
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;

        for (int i = 0; i < segment_on_screen; i++)
        {
            Spawn();
        }
    }

    private void Spawn()
    {
        // 타일 랜덤 생성
        int randomIndex = Random.Range(0, segmentPrefabs.Length);
        var go = Instantiate(segmentPrefabs[randomIndex]);

        // 만들어진 타일은 타일 매니저의 자식 오브젝트가 됩니다.
        go.transform.SetParent(transform);

        // 만들어진 타일의 위치를 설정합니다.
        go.transform.position = Vector3.forward * spawnZ;

        // 생성 위치가 타일 길이 기준으로 계속 증가(크기에 맞게 생성)
        spawnZ += segmentLength;

        // 세그먼트 리스트에 등록
        segments.Add(go);
    }

    private void Update()
    {
        // 플레이어가 일정 거리 이상 이동하게 되면 세그먼트를 생성하고, 지나갔던 타일을 제거
        if (player_transform.position.z - passZone > (spawnZ - segment_on_screen * segmentLength))
        {
            Spawn();
            Release();
        }
    }

    private void Release()
    {
        // 가장 처음에 생성한 세그먼트 제거
        Destroy(segments[0]);

        // 세그먼트 리스트의 첫 번째 값 제거
        segments.RemoveAt(0);
    }
}
