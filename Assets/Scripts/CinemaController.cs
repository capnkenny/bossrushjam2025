using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class CinemaController : MonoBehaviour
{
    [SerializeField] public float CinematicDuration;
    [SerializeField] public int SceneToTravelTo;

    public PlayableDirector CinematicTimeline;
    
    private float _timer = 0;
    private bool _playing = false;
    private bool _doneTransitioning = false;

    void Update()
    {
        //Not currently using transitions, so we'll ignore the commented bits for now.
        //if (GetAnimationFinished())
        //{
        //    _doneTransitioning = true;
        //}

        //if (_doneTransitioning)
        //{
            CinematicTimeline.Play();
            _playing = true;
            _doneTransitioning = false;
        //}

        if (_playing)
        {
            _timer += Time.deltaTime;
            if (CinematicTimeline.state != PlayState.Playing || (CinematicTimeline.state == PlayState.Playing && _timer >= CinematicDuration))
            {
                SceneManager.LoadScene(SceneToTravelTo);
            }
        }

    }
}
