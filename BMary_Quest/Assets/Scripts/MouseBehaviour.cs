using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseBehaviour : MonoBehaviour
    , IPointerEnterHandler
    , IPointerExitHandler
{
    public Texture2D baseCursor;
    public Texture2D hoverCursor;
    public Texture2D markerCursor;
    public Texture2D staffCursor;
    public GameControl gameControl;

    private bool cursorNotBase;
    private void Start()
    {
        baseCursor = (Texture2D)Resources.Load("Sprites/GUI/GUI_Mouse/Mouse_Base");
        hoverCursor = (Texture2D)Resources.Load("Sprites/GUI/GUI_Mouse/Mouse_Hover");
        markerCursor = (Texture2D)Resources.Load("Sprites/GUI/GUI_Mouse/Mouse_Marker");
        staffCursor = (Texture2D)Resources.Load("Sprites/GUI/GUI_Mouse/Mouse_Staff");
        gameControl = GameObject.Find("PController").GetComponent<GameControl>();
        
        Cursor.SetCursor(baseCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Cursor.SetCursor(hoverCursor, Vector2.zero, CursorMode.Auto);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Cursor.SetCursor(baseCursor, Vector2.zero, CursorMode.Auto);
    }

    public void Update()
    {
        if (gameControl.placeMode && gameControl.playerTurn != (int)Player_Turn.enemy)
        {
            Cursor.SetCursor(markerCursor, Vector2.zero, CursorMode.Auto);
            cursorNotBase = true;
            
        }
        else if (gameControl.staffMode && gameControl.playerTurn != (int)Player_Turn.enemy)
        {
            Cursor.SetCursor(staffCursor, Vector2.zero, CursorMode.Auto);
            cursorNotBase = true;
        }
        else if (cursorNotBase)
        {
            Cursor.SetCursor(baseCursor, Vector2.zero, CursorMode.Auto);
            cursorNotBase = false;
        }
    }
}
