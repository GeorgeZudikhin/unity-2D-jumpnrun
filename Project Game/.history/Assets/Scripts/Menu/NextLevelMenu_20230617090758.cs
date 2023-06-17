public class NextLevelMenu : MonoBehaviour
{

    public GameObject NextLevelMenuUI;

    public void Continue()
    {
        // Load next Level
        Debug.Log("Starting new Level...");
        Time.timeScale = 1f;
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex+1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}