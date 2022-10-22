using UnityEngine;

public class PauseScript : MonoBehaviour
{
    bool isPaused = false;
    public GameObject pauseGo;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Pause"))
        {
            isPaused = !isPaused;
            pauseGo.SetActive(isPaused);
            Time.timeScale = isPaused ? 0f : 1f;
        }
    }
}
