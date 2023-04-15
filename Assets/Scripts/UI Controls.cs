using System;
using UnityEngine;

public class UIControls : MonoBehaviour
{
    public event EventHandler<selectionEventArgs> OnSelection;
    [SerializeField] private Selection selection;

    public class selectionEventArgs : EventArgs
    {
        public String triggeringGameObject { get; set; }
    }

    public void OnButtonPress(String triggeringGameObject)
    {
        Debug.Log(triggeringGameObject);
        OnSelection?.Invoke(this, new selectionEventArgs() { triggeringGameObject = triggeringGameObject });
        
    }
 
}
