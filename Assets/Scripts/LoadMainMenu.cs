using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour
{
    public void OnClick()
    {
        PlayerPrefs.Save();
        SceneManager.LoadScene(1);
    }
}
