using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    // Use this for initialization
    void Start()
    {
        canvasGroup = GetComponentInParent<CanvasGroup>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClickStartBtn()
    {
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        SceneManager.LoadScene("GameScene");
    }
}
