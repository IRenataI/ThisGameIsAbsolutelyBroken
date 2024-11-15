using UnityEngine.Events;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public Texture2D cursorTexture;
    Vector2 hotSpot = new Vector2(0, 0);
    public UnityEvent onClickAction;

    private bool isInteractable = true;

    private void OnMouseEnter()
    {
        Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
    }

    private void OnMouseExit()
    {
        Cursor.SetCursor(null, hotSpot, CursorMode.Auto);
    }

    private void OnMouseDown()
    {
        if (onClickAction != null && isInteractable)
        {
            Cursor.SetCursor(cursorTexture, hotSpot, CursorMode.Auto);
            OnMouseExit();
            onClickAction.Invoke();

            if (CompareTag("Ñapybara") && CapybaraCounter.Instance != null)
            {
                CapybaraCounter.Instance.IncrementCount();
            }
            if (CompareTag("Whale") && Music.Instance != null)
            {
                Music.Instance.ChangeMusic1();
            }
            if (CompareTag("Cat") && Music.Instance != null)
            {
                Music.Instance.ChangeMusic2();
            }
        }
    }

    public void SetInteractionEnabled(bool enabled)
    {
        isInteractable = enabled;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public void PlayDialogue(int ID)
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.StartDialogue(ID); 
        }
    }
}
