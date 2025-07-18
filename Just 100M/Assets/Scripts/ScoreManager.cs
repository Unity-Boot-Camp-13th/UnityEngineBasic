using TMPro;
using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour
{
    public TextMeshProUGUI startCounting;

    // 점수
    // 1. 인게임 내에서 간단하게 사용할 거면 필드로 만든다.
    // 2. 게임을 끄고 난 뒤에도 점수가 유지되어야 하는 상황일 경우라면
    // 다음과 같은 방법들을 고려한다.
    // 2-1. PlayerPrefs      : 유니티의 데이터를 레지스트리에 저장할 때 사용합니다.
    //                         정수, 실수, 문장 정도의 간단한 데이터 저장 가능
    //                         따로 제거 안 하면 게임을 삭제해도 남아있는 경우가 허다함
    //
    // 2-2. Json             : JavaScript Object Notation ( 데이터 전송용 파일 )
    //                         객체, 배열, 문자열, Boolean, Null, 숫자 등의 데이터 유형 제공
    //                         주로 게임에서 딕셔너리나 리스트 등을 통해서 아이템 테이블, 인벤토리, 몬스터 테이블 등을
    //                         구현하려고 할 때 편하게 사용하는 용도
    //
    // 2-3. Firebase         : 데이터베이스와의 연동을 통해 데이터를 관리합니다. (멀티 게임)
    //
    // 2-4. ScriptableObject : 유니티 내부에서 Asset 의 형태로써 데이터를 저장해서 사용
    //                         인게임 데이터를 구성할 때 가장 쉽고 편리합니다.
    // 
    // 2-5. CSV              : 엑셀 파일에 필요한 데이터들을 나열해두고, C# 스크립트를 통해 해당 값을 얻어와서 적용합니다.
    //                         주로 맵 패턴, 스크립트 대화 호출, 기본적인 데이터
    

    // 남은 시간
    private float time = 64;

    // 타임 오버
    [HideInInspector] public bool timeOver = false;

    // 레벨당 요구 점수
    private int levelperscore = 100;

    // 텍스트 UI
    public TextMeshProUGUI timeText;    // 남은 시간

    private void Start()
    {
        StartCoroutine(C_Init());
        timeText.text = $"남은 시간 : 60초";
    }
    // String Format $
    // $"{변수}" 를 적을 경우 해당 변수가 문자열로 넘어가게 됩니다.
    // {숫자 : NO} : 문장 출력시 천 단위를 표시할 수 있ㅅ브니다. 1000 -> 1,000
    // #,##0 : #은 선택. 숫자가 있으면 표시, 없으면 표시 안 함.
    //         0은 필수, 숫자가 있으면 기입한 숫자 표시, 없으면 0표시.

    private void Update()
    {
        // 처음 진입했을 때 카메라 무빙
        if (Time.timeSinceLevelLoad < CameraController.camera_animate_duration)
        {
            return;
        }

        SetTMPText();

        if (time <= 0f)
        {
            timeOver = true;
            time = 0f;
        }
    }

    // void LevelUp()
    // {
    //     if (level == max_level)
    //         return;
    // 
    //     levelperscore *= 3;
    //     level++;
    //     SetTMPText();
    // }

    // public void SetTMPText()
    // {
    //     levelText.text = $"Level : {level}";
    //     perScoreText.text = $"Goal : {levelperscore: #,##0}";
    //     
    // }

    void SetTMPText()
    {
        time -= Time.deltaTime;
        timeText.text = $"남은 시간 : {(int)time}초";
    }

    IEnumerator C_Init()
    {
        WaitForSeconds waitfor1seconds = new WaitForSeconds(1);

        for (int i = 3; i >= 1; i--)
        {
            startCounting.text = i.ToString();
            Debug.Log($"남은시간 : {i}");
            yield return waitfor1seconds;
        }

        startCounting.fontSize = 200;
        startCounting.text = "Start";
        yield return waitfor1seconds;
        startCounting.enabled = false;
    }
}
