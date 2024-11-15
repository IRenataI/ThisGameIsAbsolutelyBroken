using UnityEngine;
using UnityEngine.Events;

public class ObjectDestroyOnHover : MonoBehaviour
{
    public Texture2D cursorTexture;
    Vector2 hotSpot = new Vector2(0, 0);
    public UnityEvent onClickAction;
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
        if (onClickAction != null)
        {
            onClickAction.Invoke();
        }
    }

    private void Destroy()
    {
        Destroy(gameObject);
    }
}
