using UnityEngine;

public class PickerSound : MonoBehaviour
{
    public AudioSource source;
    public AudioClip clip;
    public Transform wheelTransform;
    private float rot = 0;

    void Update()
    {
        rot = wheelTransform.eulerAngles.z;
        Debug.Log(rot);
        CheckRotation();
    }

    private void Play()
    {
        if(source && clip && !source.isPlaying)
        {
            source.PlayOneShot(clip);
        }
    }

    private void CheckRotation()
    {
        if ((rot > 40 && rot <= 50) || 
            (rot > 130 && rot <= 140) ||
            (rot > 220 && rot <= 230) || 
            (rot > 310 && rot <= 320))
            Play();
    }
}
