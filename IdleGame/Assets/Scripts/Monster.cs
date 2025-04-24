using System.Collections;
using UnityEngine;

// ������Ʈ (Component)
// ����Ƽ ������Ʈ�� ����� ���
// �����Ǵ� ������Ʈ�� �ְ�, ��ũ��Ʈ�� ���� ����ڰ� ������ִ� ����� ����
// ������Ʈ�ν� Ȱ���� �����մϴ�. (Mono ���)

// MonoBehaviour ���
// 1. ����Ƽ ������Ʈ�� �ش� Ŭ������ ������Ʈ�ν� ����� �� �ֽ��ϴ�.


public class Monster : MonoBehaviour
{
    // ����Ƽ �ν����Ϳ� �ش� �ʵ� ���� ���� ���� ����
    [Range(1, 5)] public float speed;
    GameObject Player;

    Animator animator;

    // ���� Ŭ�������� ��Ȳ�� �°� �ִϸ��̼��� �����Ű�� �մϴ�.
    // �� �� �ʿ��� �����ʹ� �����ϱ��?
    // 1. Animation
    // 2. Animator

    bool isSpawn = false;

    // ���Ͱ� �������� �� ������ �۾� (����)
    // ������ Ŀ���� ����
    IEnumerator OnSpawn()
    {
        float current = 0f; // ������ �� �����
        float percent = 0f; // �ݺ����� ���ǿ�, �ִ� 1
        float start = 0f; // ��ȭ ���� ��
        float end = transform.localScale.x; // ��ȭ ������ ��
        // localScale �� ���� ������Ʈ�� ������� ũ�⸦ �ǹ��մϴ�.
        // ����� ������Ʈ�� ũ��� ����մϴ�.

        while (percent < 1f)
        {
            current += Time.deltaTime;
            percent = current / end;

            // start ���� end �������� percent �������� �̵��ض�
            var pos = Mathf.Lerp(start, end, percent);
            transform.localScale = new Vector3(pos, pos, pos);

            // Ż���ߴٰ� ���ƿɴϴ�.
            yield return null;
            isSpawn = true;
        }
    }


    private void Start()
    {
        animator = GetComponent<Animator>();
        // �ڵ� ������ Animator �� �ν��ϰ�, Animator�� �ʵ峪 �޼ҵ带
        // ����� �� �ֽ��ϴ�.
        GameObject playerObj = GameObject.FindWithTag("Player");
        if (playerObj != null)
        {
            Player = playerObj;
        }
        StartCoroutine(OnSpawn());
    }


    // ����Ƽ ������ ����Ŭ �Լ�
    private void Update()
    {
        transform.LookAt(Player.transform.position);

        if (isSpawn == false)
            return;

        var distance = Vector3.Distance(transform.position, Player.transform.position);

        // ������ ���غ��� ���� �Ÿ��� ������
        if (distance <= 0.5f)
        {
            SetAnimator("isIDLE"); // ������ �����մϴ�.
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Player.transform.position, Time.deltaTime * speed);
            SetAnimator("isMOVE"); // �̵����� �����մϴ�.

        }
        #region �ʱ�
        // 1. transform.position : ���� ������Ʈ�� ��ġ�� ��Ÿ���ϴ�.
        // 2. Vector3 : 3D ȯ���� ��ǥ�� (X,Y,Z ��) ����
        // 3. MoveTowards(start, end, speed) : start ���� end �������� speed ��ġ��ŭ �̵��մϴ�.
        // 4. Time.deltaTime : ���� �������� �Ϸ�Ǳ���� �ɸ� �ð�
        //                     (��ǻ���� ������ �������� ���� Ŀ��)
        //                     �Ϲ������� �� 1��
        //                     ������Ʈ���� �۾��� �ϴµ� �־�� ���� �� ����
        // 5. transform.LookAt(Vector3 position) : Ư�� ������ �ٶ󺸰� �������ִ� ���

        // ���� ���� : �⺻������ �������ִ� Vector ��
        // Vector3.right   == new Vector3(1,0,0);
        // Vector3.left    == new Vector3(-1,0,0);
        // Vector3.up      == new Vector3(0,1,0);
        // Vector3.down    == new Vector3(0,-1,0);
        // Vector3.forward == new Vector3(0,0,1);
        // Vector3.back    == new Vector3(0,0,-1);
        // Vector3.zero    == new Vector3(0,0,0);
        // Vector3.one     == new Vector3(1,1,1); 
        #endregion
    }

    private void SetAnimator(string temp)
    {
        // �⺻ �Ķ���Ϳ� ���� �ʱ�ȭ
        // ����Ƽ Animator �� ����� �� parameter �� �̸��� ��Ȯ�ϰ� �����մϴ�.
        animator.SetBool("isIDLE", false);
        animator.SetBool("isMove", false);

        // ���ڷ� ���޹��� ���� true �� ����
        animator.SetBool(temp, true);
    }
}