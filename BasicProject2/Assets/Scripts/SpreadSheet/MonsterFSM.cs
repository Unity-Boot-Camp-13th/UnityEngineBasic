using System;
using UnityEngine;
using UnityEngine.AI;

// MonsterFSM 코드 요약
// 1. 이동 담당 : NavMesh Agent
// 2. 상태 전환 담당 (FSM) : Patrol <-> Chase
// 3. 목적지 : 플레이어
// 4. 추가 사항 : Patrol 에 필요한 이동 경로

public class MonsterFSM : MonoBehaviour
{
    // 1. 상태 표현 (Enum)
    public enum MonsterState { Patrol, Chase }

    // 2. 필드 선언
    [Header("몬스터의 상태")]
    public MonsterState State;

    public Transform[] points; // 정찰 위치
    int current_idx = 0;

    [Header("플레이어")]
    public Transform player;

    [Header("정찰 범위")]
    public float range = 3.0f;
    private NavMeshAgent navMeshAgent; // 선언

    // 3. 기본 상태 등록
    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // 등록 (초기화)
        State = MonsterState.Patrol;
        NextPoint();
    }

    // 4. 메인 로직 구현
    private void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position);

        switch (State)
        {
            case MonsterState.Patrol:
                if (distance >= range)
                {
                    // 5. 상태에 맞는 움직임 함수에 대한 구현
                    Patrol();
                }
                else
                {
                    // 6. 상태를 변환할 코드 대기
                    State = MonsterState.Chase;
                }
                break;

            case MonsterState.Chase:
                if (distance <= range)
                {
                    Chase();
                }
                else
                {
                    State = MonsterState.Patrol;
                    NextPoint();
                }
                break;
        }
    }

    private void Chase()
    {
        navMeshAgent.destination = player.position;
    }

    private void Patrol()
    {
        // 경로에 대한 계산이 완료된 상태고,
        // 목적지까지 남은 거리가 0.5f 이하라면
        // 다음 지점으로 이동
        if (!navMeshAgent.pathPending &&
            navMeshAgent.remainingDistance < 0.5f)
        {
            NextPoint();
        }
    }

    private void NextPoint()
    {
        if (points.Length == 0)
            return;

        navMeshAgent.destination = points[current_idx].position;
        current_idx = (current_idx + 1) % points.Length;
    }
}
