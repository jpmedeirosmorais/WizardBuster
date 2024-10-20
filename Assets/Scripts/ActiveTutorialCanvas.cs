using UnityEngine;

public class ActiveTutorialCanvas : MonoBehaviour
{
    public GameObject tutorialCanvas;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            tutorialCanvas.SetActive(true);
            Time.timeScale = 0f;
            Destroy(gameObject);
        }
    }
}
