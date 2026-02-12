using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    public void SlideIn()
    {
        animator.SetTrigger("SlideIn");
    }

    public void SlideOut()
    {
        animator.SetTrigger("SlideOut");
    }

}
