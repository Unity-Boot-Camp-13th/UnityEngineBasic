using System;
using UnityEngine;
using System.Collections;

public class Player : Unit
{
    Vector3 pos; // 좌표계
    Quaternion quat; // 회전 값
    GameObject attack;

    protected override void AttackObject()
    {
        if (target.TryGetComponent<Monster>(out var u) && u.HP <= 0)
        {
            Debug.Log("몬스터죽음");
            // 공격 트리거 취소
            if (attack != null)
            {
                Manager.Pool.pool_dict["Attack"].Release(attack);
                attack = null;
            }
            animator.ResetTrigger("isATTACK");
            animator.SetBool("isATTACK", false);
            SetAnimator("isIDLE");
            return;
        }
        else
        {
            animator.SetTrigger("isATTACK");

            Manager.Pool.Pooling("Attack").get((value) =>
            {
                attack = value;
                value.transform.position = attack_transform.position;
                // 일반적으로 무기의 맨 앞 부분쪽을 위치로 잡습니다.
                value.GetComponent<Attack>().Init(target, 1, "ATK01");
            });
        }
    }
    


    protected override void Start()
    {
        base.Start();
        pos = transform.position;
        quat = transform.rotation;
    }

    // 메인 로직을 짜는 위치 (Update)
    private void Update()
    {
        StrikeFirst(Spawner.monster_list.ToArray());
        // 리스트 -> 배열

        // 타겟이 없다?
        if (target == null)
        {
            // 거리 계산
            var targetPos = Vector3.Distance(transform.position, pos);

            if (targetPos > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, pos, Time.deltaTime);
                transform.LookAt(pos);
                SetAnimator("isMOVE");
            }
            else
            {
                transform.rotation = quat;
                SetAnimator("isIDLE");
            }
            return;
        }

        // 타겟 거리 설정
        var targetDistance = Vector3.Distance(transform.position, target.position);

        // 사정 거리 안에 들어왔을 경우 (공격 사정 범위에는 포함이 안 되는 경우)
        if (targetDistance <= T_RANGE &&
            targetDistance > A_RANGE)
        {
            SetAnimator("isMOVE");
            transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime);
        }
        else if (targetDistance <= A_RANGE)
        {
            SetAnimator("isATTACK");
        }

        if (target == null)
        {
            animator.SetBool("isATTACK", false);
        }
    }

    
}