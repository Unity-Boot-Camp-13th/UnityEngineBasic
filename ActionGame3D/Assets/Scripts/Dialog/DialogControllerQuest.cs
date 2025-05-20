using UnityEngine.UI;
using UnityEngine;

namespace Assets.Scripts.Dialog
{
    public class DialogControllerQuest : DialogController
    {
        public Text LabelTitle;
        public Text LabelMessage;
        public Text QuestContent;

        public DialogDataQuest Data { get; private set; }

        public override void Awake()
        {
            base.Awake();
        }

        public override void Start()
        {
            base.Start();
            DialogManager.Instance.Regist(DialogType.Quest, this);
        }

        public override void Build(DialogData data)
        {
            base.Build(data);

            // 데이터 여부 확인
            if (!(data is DialogDataQuest))
            {
                Debug.LogError("유효하지 않은 다이얼로그 데이터");
                return;
            }
            // 메시지 등록
            Data = data as DialogDataQuest;
            LabelTitle.text = Data.Title;
            LabelMessage.text = Data.Message;
        }

        public void OnOKButtonClick()
        {
            // 데이터도 있고, 콜백도 요청했다면
            if (Data != null && Data.Callback != null)
            {
                Data.Callback();
            }
            // 콜백 이후, 매니저에서 제거
            DialogManager.Instance.Pop();

            QuestContent.text = "슬라임 10마리 처치 (0/10)";
        }

        public void OnQuestCheckButtonClick()
        {
            DialogDataQuest quest = new DialogDataQuest("퀘스트", "슬라임 10마리를 제거하세요.",
                                                    () =>
                                                    {
                                                        Debug.Log("버튼을 눌러주세요.");
                                                    });
            DialogManager.Instance.Push(quest);
        }
    }
}