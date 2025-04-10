using TMPro;
using UnityEngine;

namespace Match3.InGame.LevelSystems
{
    public class InGameScore : MonoBehaviour
    {
        [SerializeField] ScoringText _scoringText;
        [SerializeField] TextMeshPro _comboStack;
        [SerializeField] TextMeshPro _comboLable;
        [SerializeField] Map _map;

        private void Start()
        {
            _map.OnScoreChanged += (score, comboStack) =>
            {
                _scoringText.Score = score;

                if (comboStack > 0)
                {
                    _comboLable.text = "Combo";
                    _comboStack.text = (comboStack + 1).ToString();
                }
                else
                {
                    _comboLable.text = string.Empty;
                    _comboStack.text = string.Empty;
                }
            };
        }

    }
}