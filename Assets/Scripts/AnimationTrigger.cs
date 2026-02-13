using UnityEngine;
using UnityEngine.UI; // Required for UI elements

public class AnimationTrigger : MonoBehaviour
{
    [SerializeField]
    public Animator animator;

    public void SlideIn()
    {
        if(DataHolder.Instance.trainSessions > 0)
        {
            animator.SetTrigger("SlideIn");
        }
        else
        {
            PersistentUI.instance.ChangeTrainText();
        }  
    }

    public void SlideOut()
    {
        animator.SetTrigger("SlideOut");
    }

}
