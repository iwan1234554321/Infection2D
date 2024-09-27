using Notteam.World;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameSystem : WorldSystem
{
    [SerializeField] private string backMusicName;

    [Header("References")]
    [SerializeField] private CanvasGroup gameOverPanel;

    private bool _isPaused;

    private string _currentLevelName;

    public bool IsPaused => _isPaused;

    private void ShowGameOverPanel(bool instant = false)
    {
        if (instant)
        {
            gameOverPanel.alpha = 1.0f;

            gameOverPanel.interactable = true;
            gameOverPanel.blocksRaycasts = true;
        }
        else
        {
            var startAlpha = gameOverPanel.alpha;

            gameObject.CreateTween(new Tween("ShowGameOverPanel", 0.25f,
                onUpdate: (t) =>
                {
                    gameOverPanel.alpha = Mathf.Lerp(startAlpha, 1.0f, t);
                },
                onFinal: () =>
                {
                    gameOverPanel.interactable = true;
                    gameOverPanel.blocksRaycasts = true;
                }));
        }
    }

    private void HideGameOverPanel(bool instant = false)
    {
        if (instant)
        {
            gameOverPanel.alpha = 0.0f;

            gameOverPanel.interactable = false;
            gameOverPanel.blocksRaycasts = false;
        }
        else
        {
            var startAlpha = gameOverPanel.alpha;

            gameObject.CreateTween(new Tween("ShowGameOverPanel", 0.25f,
                onUpdate: (t) =>
                {
                    gameOverPanel.alpha = Mathf.Lerp(startAlpha, 0.0f, t);
                },
                onFinal: () =>
                {
                    gameOverPanel.interactable = false;
                    gameOverPanel.blocksRaycasts = false;
                }));
        }
    }

    protected override void OnStart()
    {
        _currentLevelName = SceneManager.GetActiveScene().name;

        HideGameOverPanel(true);

        World.Instance.GetSystem<PoolObjectSystem>().Create(backMusicName, transform.position, transform.rotation);

        World.Instance.GetSystem<EventDispatcherSystem>().SubscribeToEvent<GameOverEvent>(GameOver);
    }

    protected override void OnFinal()
    {
        World.Instance.GetSystem<EventDispatcherSystem>().UnSubscribeFromEvent<GameOverEvent>(GameOver);
    }

    private void GameOver(GameOverEvent @event)
    {
        Time.timeScale = 0.0f;

        ShowGameOverPanel();
    }

    public void Restart()
    {
        Time.timeScale = 1.0f;

        SceneManager.LoadScene(_currentLevelName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
