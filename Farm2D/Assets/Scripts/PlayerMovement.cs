using System;
using UnityEngine;

[Serializable]
public class PlayerStat
{
    public float speed;
    // public int count_of_harvest; // 현재 수확물의 개수
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerStat stat;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var h = Input.GetAxisRaw("Horizontal");
        var v = Input.GetAxisRaw("Vertical");
        Vector3 dir = new Vector3(h, v);

        transform.position += dir * Time.deltaTime * stat.speed;

        SetAnimateMovement(dir);
    }

    void SetAnimateMovement(Vector3 direction)
    {
        if (animator != null)
        {
            // magnitue : 벡터의 길이
            // x, y, z 에 대한 각각의 제곱의 합의 루트 값
            if (direction.magnitude > 0)
            {
                animator.SetBool("IsMove", true);

                animator.SetFloat("Horizontal", direction.x);
                animator.SetFloat("Vertical", direction.y);
            }
            else
            {
                animator.SetBool("IsMove", false);
            }
        }
    }
}
