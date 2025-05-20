using System;

namespace Assets.Scripts.Dialog
{
    public class DialogDataQuest : DialogData
    {
        public string Title { get; private set; }
        public string Message { get; private set; }
        public Action Callback { get; private set; }

        public DialogDataQuest(string title, string message, Action callback) : base(DialogType.Quest)
        {
            Title = title;
            Message = message;
            Callback = callback;
        }
    }
}