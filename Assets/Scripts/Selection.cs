using System;
using UnityEngine;
using UnityEngine.EventSystems;
//Original Code was obtained from https://github.com/DA-LAB-Tutorials/YouTube-Unity-Tutorials/blob/main/Selection.cs but modified in order to work with the Quick Outline Assest Obtained from the Unity Asset Store
public class Selection : MonoBehaviour
{
    private Transform outline;
    private Transform selection;
    private RaycastHit raycastHit;
    public event EventHandler<selectionEventArgs> OnSelection;
    void Update()
    {
        if (outline != null && !EventSystem.current.IsPointerOverGameObject() && outline !=selection)
        {
            if (outline.TryGetComponent(out Outline outlineComponent))
            {
                outlineComponent.enabled = false;
                outline = null;
            }
        }

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!EventSystem.current.IsPointerOverGameObject() && Physics.Raycast(ray, out raycastHit)) //Make sure you have EventSystem in the hierarchy before using EventSystem
        {
            outline = raycastHit.transform;
            if (outline.CompareTag("Selectable") && outline != selection)
            {
                if (outline.TryGetComponent(out Outline outlineComponent))
                {

                    outlineComponent.enabled = true;

                }
            }
            else if(!EventSystem.current.IsPointerOverGameObject())
            {
                outline= null;

            }
        }

        // Selection
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
        {
            if (outline)
            {
                if (selection && selection.TryGetComponent(out Outline outlineComponent))
                {
                    outlineComponent.enabled = false;
                }
                selection = raycastHit.transform;
                if (selection.TryGetComponent(out Outline outlineSelection))
                {

                    outlineSelection.enabled = true;
                    OnSelection?.Invoke(this, new selectionEventArgs() { triggeringGameObject = selection });
                }
                outline = null;
            }
            else
            {

                ClearSelection();
                OnSelection?.Invoke(this, new selectionEventArgs() { triggeringGameObject = null }); ;
            }
        }


    }

    public void ClearSelection()
    {
        if (selection)
        {
            if (selection.TryGetComponent(out Outline outline))
            {
                outline.enabled = false;
                selection = null;
            }
        }
    }

    public void SetSelection(string triggeringGameObject)
    {
        selection = GameObject.Find(triggeringGameObject).transform;
    }

    public class selectionEventArgs : EventArgs
    {
        public Transform triggeringGameObject { get; set; }
    }


    public void SelectOnButtonMouseOver(String triggeringObject)
    {
        GameObject.Find(triggeringObject).GetComponent<Outline>().enabled = true;
        outline = GameObject.Find(triggeringObject).transform;

    }
}