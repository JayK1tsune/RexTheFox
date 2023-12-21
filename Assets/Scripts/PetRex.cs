using UnityEngine;

public class PetRex : MonoBehaviour
{
    [SerializeField] private GameObject petRex;
    [SerializeField] private UI_Manager uiManager;

    private Collider foxCollider; // Reference to the collider component
    private bool isMouseOver;
    private float hoverTimer;
    public bool hasPetRexed;
    private float requiredHoverTime = 2f; // Time in seconds to trigger the animation

    public Texture2D cursorTexture;
    public Texture2D cursorTexture2;

    void Start()
    {
        // Assigning references in Start() instead of Awake()
        petRex = GameObject.Find("Fox");
        uiManager = GameObject.Find("TextManager").GetComponent<UI_Manager>();

        // Get the collider component of the fox
        foxCollider = petRex.GetComponent<Collider>();
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        hasPetRexed = false;
    }

    void Update()
    {
        if (isMouseOver)
        {
            Cursor.SetCursor(cursorTexture2, Vector2.zero, CursorMode.ForceSoftware);
            // Increment the timer while the mouse is over the collider
            hoverTimer += Time.deltaTime;

            if (hoverTimer >= requiredHoverTime)
            {
                PlayPattingAnimation();
                hasPetRexed = true;
                ResetTimerAndMouseState();
            }
        }
    }

    void OnMouseOver()
    {
        isMouseOver = true;

        if (hoverTimer >= requiredHoverTime)
        {
            // Reset timer if mouse stayed long enough previously and re-entered
            ResetTimerAndMouseState();
        }
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
        ResetTimerAndMouseState();
    }

    void ResetTimerAndMouseState()
    {
        isMouseOver = false;
        hoverTimer = 0f;
    }

    void PlayPattingAnimation()
    {
        // Play animation when the mouse hovers over the collider for the required duration
        petRex.GetComponent<Animator>().Play(uiManager.talkingAnimations[0].name);
        uiManager.talkingAudioSource.PlayOneShot(uiManager.talkingClips[0]);
        Debug.Log("Patting animation played");
    }
}






