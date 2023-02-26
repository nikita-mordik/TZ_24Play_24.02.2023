using CodeBase.Core;
using TMPro;
using UnityEngine;

namespace CodeBase.UI
{
    public class PointCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI counterText;

        private int totalPoint;

        private void Start()
        {
            EventListener.Instance.OnAddPoint += UpdateCounter;
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnAddPoint -= UpdateCounter;
        }

        private void UpdateCounter(int point)
        {
            totalPoint += point;
            counterText.text = $"Total points: {totalPoint}";
        }
    }
}