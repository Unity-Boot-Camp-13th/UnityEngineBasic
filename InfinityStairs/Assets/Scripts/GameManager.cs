using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Character; // 캐릭터
    public GameObject Stair;     // 계단(발판)
    public Transform Stairs_Parent; // 계단 모아줄 위치
    public float StairOffsetX = 1.2f;
    public float StairOffsetY = 0.6f;

    private int stair_pos_idx = 0; // 발판 위치에 따른 인덱스 값
    private int character_pos_idx = 0; // 캐릭터 위치에 따른 인덱스 값
    private bool is_playing = false; // 실행 유무

    // 플랫폼 리스트 (배치되어 있는 판)
    private List<GameObject> stair_list = new();

    // 플랫폼에 대한 체크 리스트
    private List<int> stair_check_list = new();

    private void Start()
    {
        SetFlatform(); // 발판 설정
        Init(); // 게임 초기화
    }

    private void Update()
    {
        // 플레이 모드라면
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
        // 체크 확인용 코드
        Debug.Log("인덱스 값 : " + idx);

        if (stair_check_list[idx] == direction)
        {
            // 캐릭터의 위치 변경
            character_pos_idx++;
            Character.transform.position = (Vector2) stair_list[idx].transform.position +
                                           new Vector2(0, 0.4f);
            // 바닥 설정
            NextStair(stair_pos_idx);
        }
        else
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("게임 오버");
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
        is_playing = true; // 이후에는 플레이 버튼을 눌렀을 때 진행되도록 수정
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
            else // 인덱스 범위를 넘은 경우
            {
                // 왼쪽 발판
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
                else // 오른쪽 발판
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
        // 임의의 숫자만큼 플랫폼 생성
        for (int i = 0; i < 20; i++)
        {
            GameObject plat = Instantiate(Stair, Vector3.zero, Quaternion.identity, Stairs_Parent);
            stair_list.Add(plat);
            stair_check_list.Add(0);
        }
    }
}
