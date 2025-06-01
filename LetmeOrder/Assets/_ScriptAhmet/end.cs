using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class end : MonoBehaviour
{
    private void Start()
    {
        VideoPlayer vp = GetComponent<VideoPlayer>();
        vp.loopPointReached += OnVideoFinished;
    }

    private void OnVideoFinished(VideoPlayer vp)
    {
        SceneManager.LoadScene("AnaMenu");
    }
}
