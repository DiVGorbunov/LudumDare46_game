using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName = "";

    private bool IsWinScene
    {
        get
        {
            return string.Compare(SceneManager.GetActiveScene().name, "WinScene", true) == 0;
        }
    }

    private void Start()
    {
        if (IsWinScene && DifficultyManager.Instance.IsMaxLevel)
        {
            var textMesh = GetComponentInChildren<TextMeshProUGUI>();
            if (textMesh.text == "Continue")
            {
                textMesh.text = "Play Again";
                var titleTextMesh = gameObject.transform.parent.GetComponentInChildren<TextMeshProUGUI>();
                if (titleTextMesh != null)
                {
                    titleTextMesh.text = "You WIN the Game, congratulations!";
                }
            }
        }
    }

    private void Update()
    {
        if(EventSystem.current.currentSelectedGameObject == gameObject 
            && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
        {
            LoadTargetScene();
        }
    }

    public void LoadTargetScene()
    {
        if (IsWinScene)
        {
            DifficultyManager.Instance.MoveToNextLevel();
        }
        SceneManager.LoadScene(sceneName);
    }
}
