using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
using Assets.Scripts.Dialog;

public class StageController : MonoBehaviour
{
    public static StageController Instance; // 매니저 정적 변수

    public int StagePoint = 0; // 점수

    public Text PointText; // 점수 표현할 UI
    public Text QuestContent;

    public Image fadeInOut;
    public float duration = 5.0f;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        // 페이드 인
        if (fadeInOut != null)
        {
            fadeInOut.gameObject.SetActive(true);
            Color c = fadeInOut.color;
            fadeInOut.color = new Color(c.r, c.g, c.b, 1.0f);
            StartCoroutine(FadeIn());
        }

        // 안내문 데이터 콜백
        DialogDataAlert alert = new DialogDataAlert("Start", "10초마다 생성되는 슬라임들을 제거하세요.",
                                                    () =>
                                                    {
                                                        Debug.Log("버튼을 눌러주세요.");
                                                    });
        DialogManager.Instance.Push(alert);
    }

    IEnumerator FadeIn()
    {
        yield return StartCoroutine(Fade(1, 0));
    }

    IEnumerator FadeOut()
    {
        yield return StartCoroutine(Fade(0, 1));
    }

    IEnumerator Fade(float start, float end)
    {
        float time = 0.0f;
        Color s = new Color(0, 0, 0, start);
        Color e = new Color(0, 0, 0, end);

        while (time < duration)
        {
            fadeInOut.color = Color.Lerp(s, e, time / duration);
            time += Time.unscaledDeltaTime;
            yield return null;
        }

        fadeInOut.color = e;
    }

    // 점수가 증가하면 텍스트 UI에 수치를 적용
    public void AddPoint(int Point)
    {
        StagePoint += Point;
        PointText.text = Point.ToString();

        int score = 0;
        score += 1;
        QuestContent.text = $"슬라임 10마리 처치 ({score}/10)";
    }

    // 씬에 대한 리로드
    public void FinishGame()
    {
        DialogDataConfirm confirm = new DialogDataConfirm("알림", "다시 시작하시겠습니까?",
                                    delegate (bool answer)
                                    {
                                        if (answer)
                                        {
                                            StartCoroutine(FadeOut());
                                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                                        }
                                        else
                                        {
                                            // 이전 프로젝트의 내용을 활용해서 에디터 상에서도 종료되도록 수정해주세요.
                                            #if UNITY_EDITOR
                                            {
                                                UnityEditor.EditorApplication.isPlaying = false;
                                            }
                                            #else
                                            Application.Quit();
                                            # endif
                                        }
                                    });

        // 매니저에 등록
        DialogManager.Instance.Push(confirm);
    }
}