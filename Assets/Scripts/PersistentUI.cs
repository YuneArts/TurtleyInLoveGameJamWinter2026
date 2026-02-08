using UnityEngine;

public class PersistentUI : MonoBehaviour
{
    [SerializeField]
    public static PersistentUI instance;

    private bool canPause = true;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void OnButtonClick()
    {
        Debug.Log("Button was clicked!");
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
