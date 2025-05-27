using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Character; // ĳ����
    public GameObject Stair;     // ���(����)
    public Transform Stairs_Parent; // ��� ����� ��ġ
    public float StairOffsetX = 1.2f;
    public float StairOffsetY = 0.6f;

    private int stair_pos_idx = 0; // ���� ��ġ�� ���� �ε��� ��
    private int character_pos_idx = 0; // ĳ���� ��ġ�� ���� �ε��� ��
    private bool is_playing = false; // ���� ����

    // �÷��� ����Ʈ (��ġ�Ǿ� �ִ� ��)
    private List<GameObject> stair_list = new();

    // �÷����� ���� üũ ����Ʈ
    private List<int> stair_check_list = new();

    private void Start()
    {
        SetFlatform(); // ���� ����
        Init(); // ���� �ʱ�ȭ
    }

    private void Update()
    {
        // �÷��� �����
        if (is_playing)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                Check_Platform(character_pos_idx, 1);
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                Check_Platform(character_pos_idx, 0);
            }

        }
    }

    private void Check_Platform(int idx, int direction)
    {
        // üũ Ȯ�ο� �ڵ�
        Debug.Log("�ε��� �� : " + idx);

        if (stair_check_list[idx] == direction)
        {
            // ĳ������ ��ġ ����
            character_pos_idx++;
            Character.transform.position = (Vector2) stair_list[idx].transform.position +
                                           new Vector2(0, 0.4f);
            // �ٴ� ����
            NextStair(stair_pos_idx);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("���� ����");
        is_playing = false;
    }

    private void Init()
    {
        Character.transform.position = Vector3.zero;

        for (stair_pos_idx = 0; stair_pos_idx < 20; stair_pos_idx++)
        {
            NextStair(stair_pos_idx);
        }
        character_pos_idx = 0;
        is_playing = true; // ���Ŀ��� �÷��� ��ư�� ������ �� ����ǵ��� ����
    }

    private void NextStair(int idx)
    {
        int pos = UnityEngine.Random.Range(0, 2);

        if (idx == 0)
        {
            stair_check_list[idx] = pos;
            stair_list[idx].transform.position = new Vector2(0, - 1.17f);
        }
        else
        {
            if (stair_pos_idx < 20)
            {
                if (pos == 0)
                {
                    stair_check_list[idx] = pos;
                    stair_list[idx].transform.position = (Vector2)stair_list[idx - 1].transform.position + new Vector2(-StairOffsetX, StairOffsetY);
                }
                else
                {
                    stair_check_list[idx] = pos;
                    stair_list[idx].transform.position = (Vector2)stair_list[idx - 1].transform.position + new Vector2(StairOffsetX, StairOffsetY);
                }
            }
            else // �ε��� ������ ���� ���
            {
                // ���� ����
                if (pos == 0)
                {
                    if (idx % 20 == 0)
                    {
                        stair_check_list[19] = pos;
                        stair_list[idx % 20].transform.position = (Vector2) stair_list[19].transform.position +
                                                                  new Vector2(-StairOffsetX, StairOffsetY);
                    }
                    else
                    {
                        stair_check_list[idx % 20] = pos;
                        stair_list[idx % 20].transform.position = (Vector2)stair_list[idx % 20].transform.position +
                                                                  new Vector2(- StairOffsetX, StairOffsetY);
                    }
                }
                else // ������ ����
                {
                    if (idx % 20 == 0)
                    {
                        stair_check_list[19] = pos;
                        stair_list[idx % 20].transform.position = (Vector2)stair_list[19].transform.position +
                                                                  new Vector2(StairOffsetX, StairOffsetY);
                    }
                    else
                    {
                        stair_check_list[idx % 20] = pos;
                        stair_list[idx % 20].transform.position = (Vector2)stair_list[idx % 20].transform.position +
                                                                  new Vector2(StairOffsetX, StairOffsetY);
                    }
                }
            }
            
        }
        stair_pos_idx++;
    }

    private void SetFlatform()
    {
        // ������ ���ڸ�ŭ �÷��� ����
        for (int i = 0; i < 20; i++)
        {
            GameObject plat = Instantiate(Stair, Vector3.zero, Quaternion.identity, Stairs_Parent);
            stair_list.Add(plat);
            stair_check_list.Add(0);
        }
    }
}
