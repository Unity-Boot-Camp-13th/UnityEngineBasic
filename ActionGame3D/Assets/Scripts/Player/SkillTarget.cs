using System.Collections.Generic;
using UnityEngine;

public class SkillTarget : MonoBehaviour
{
    public List<Collider> targetList;
    GameObject target;

    private void Awake()
    {
        targetList = new List<Collider>();
    }

    // 몬스터가 공격 반경으로 들어오면, 리스트 추가
    private void OnTriggerEnter(Collider other)
    {
        if (!targetList.Contains(other) &&
            other.CompareTag("Enemy"))
        {
            targetList.Add(other);
        }
    }

    // 몬스터가 공격 반경에서 벗어나면, 리스트 제거 
    private void OnTriggerExit(Collider other)
    {
        if (targetList.Contains(other) &&
            other.CompareTag("Enemy"))
        {
            targetList.Remove(other);
        }
    }

    private void LateUpdate()
    {
        targetList.RemoveAll(target => target == null);
    }
}
