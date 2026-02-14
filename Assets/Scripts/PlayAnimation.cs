using UnityEngine;

public class PlayAnimation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPunch()
    {
        GetComponent<Animator>().SetTrigger("Punch");
    }

    public void PlayTackle()
    {
        GetComponent<Animator>().SetTrigger("Tackle");
    }
}
