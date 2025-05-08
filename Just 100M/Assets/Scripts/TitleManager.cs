using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    // 컨트롤 버튼을 통해서 오픈해줄 메뉴
    public GameObject ControlKeyMenu;

    public void OnStartButtonEnter()
    {
        SceneManager.LoadScene("GameScene");
        // GameScene 으로 로드합니다
    }

    public void OnCotrolKeyButtonEnter()
    {
        // activeSelf 는 현재의 게임오브젝트가 활성 상태인지의 여부를 확인할 수 있는 프로퍼티
        if (ControlKeyMenu.activeSelf)
        {
            ControlKeyMenu.SetActive(false);
        }
        else
        {
            ControlKeyMenu.SetActive(true);
        }
    }


    // 에디터 쪽 환경에서의 종료와
    // 빌드 쪽 환경에서의 종료를 상황에 따라 처리합니다.
    public void Exit()
    {
#if UNITY_EDITOR // 유니티 에디터 쪽에서의 작업
        UnityEditor.EditorApplication.isPlaying = false; // 현재 활성화된 코드가 바로 비활성화
        // 누르면 바로 꺼지는 기능 (모바일, 빌드용)
#else
        Application.Quit();
#endif
    }
}
