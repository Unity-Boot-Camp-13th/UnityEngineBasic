using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Dialog
{
    public enum DialogType
    {
        None,
        Alert, 
        Confirm,
        Quest,
    }

    public sealed class DialogManager : MonoBehaviour
    {
        List<DialogData> _dialogList;                   // 다이얼로그 데이터 리스트
        Dictionary<DialogType, DialogController> _dialogDict; // 다이얼로그 타입에 맞는 컨트롤러 (딕셔너리)
        DialogController _currentDialogController;      // 현재의 다이얼로그 컨트롤러

        public static DialogManager Instance { get; private set; }


        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(Instance);
                return;
            }
            Instance = this;

            // 클래스에 인스턴스가 생성될 때, 리스트와 딕셔너리를 초기화합니다.
            _dialogList = new List<DialogData>();
            _dialogDict = new Dictionary<DialogType, DialogController>();
        }

        public void Regist(DialogType type, DialogController controller)
        {
            _dialogDict[type] = controller;
        }

        public void Push(DialogData data)
        {
            _dialogList.Add(data);
        
            if (_currentDialogController == null)
            {
                ShowNext();
            }
        }

        // 현재 열려있는 대화창을 닫고, 남아있는 다음 대화창을 보여주는 구조
        public void Pop()
        {
            if (_currentDialogController != null)
            {
                _currentDialogController.Close(
                    delegate
                    {
                        _currentDialogController = null;

                        if (_dialogList.Count > 0)
                        {
                            ShowNext();
                        }
                    });
            }
        }

        private void ShowNext()
        {
            DialogData next = _dialogList[0]; // 리스트의 첫번째 값을 지정

            DialogController controller = _dialogDict[next.Type].GetComponent<DialogController>();

            _currentDialogController = controller;
            _currentDialogController.Build(next);
            _currentDialogController.Show(() => { }); // 대화창을 띄우되 따로 후처리를 하지 않는다.

            _dialogList.RemoveAt(0); // 리스트에서 첫번째 값 제거
        }

        public bool IsShowing()
        {
            return _currentDialogController != null;
        }
    }
}
