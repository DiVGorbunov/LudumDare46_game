using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class LoadSceneButton : MonoBehaviour
{
    public string sceneName = "";

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == gameObject
            && Input.GetButtonDown(GameConstants.k_ButtonNameSubmit))
        {
            LoadTargetScene();
        }
    }

    public void LoadTargetScene()
    {
        if (string.Compare(SceneManager.GetActiveScene().name, "WinScene", true) == 0)
        {
            DifficultyManager.Instance.ResetLevel();
        }
        SceneManager.LoadScene(sceneName);
    }
}
