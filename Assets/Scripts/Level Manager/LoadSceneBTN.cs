using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneBTN : MonoBehaviour
{
    [SerializeField]
    private string sceneName;

    public void OnClick()
    {
        SceneManager.LoadScene(sceneName);
    }
}
