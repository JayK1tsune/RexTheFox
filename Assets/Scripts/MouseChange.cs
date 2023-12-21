using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseChange : MonoBehaviour
{
    [SerializeField] private PetRex petRex;

    
    void Start()
    {
        petRex = GameObject.Find("PatPoint").GetComponent<PetRex>();
    }


    
    public void OnMouseEnter()
    {
        Cursor.SetCursor(petRex.cursorTexture2, Vector2.zero, CursorMode.ForceSoftware);
        Debug.Log("Mouse is over GameObject.");
    }
    public void OnMouseExit()
    {
        Cursor.SetCursor(petRex.cursorTexture, Vector2.zero, CursorMode.ForceSoftware);
    }

}
