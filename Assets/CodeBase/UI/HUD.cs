using CodeBase.Core;
using CodeBase.Extensions;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CodeBase.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField] private CanvasGroup fadePanel;
        [SerializeField] private CanvasGroup startPanel;
        [SerializeField] private CanvasGroup failPanel;

        [SerializeField] private Button tryAgainButton;

        private void Start()
        {
            FadeOff();
            EventListener.Instance.OnGameStart += OnGameStart;
            EventListener.Instance.OnGameEnd += OnGameEnd;
            tryAgainButton.onClick.AddListener(Restart);
        }

        private void OnDestroy()
        {
            EventListener.Instance.OnGameStart -= OnGameStart;
            EventListener.Instance.OnGameEnd -= OnGameEnd;
        }

        private void OnGameStart() => 
            startPanel.State(false);

        private void OnGameEnd() => 
            failPanel.State(true);

        private void Restart() => 
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void FadeOff() =>
            fadePanel.DOFade(0f, 2f)
                .onComplete += () => fadePanel.State(false);
    }
}