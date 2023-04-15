using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ObjectClickLabel : MonoBehaviour
{
    [SerializeField] private Canvas canvas;         
    [SerializeField] private GameObject labelPrefab;
    private GameObject currentLabel;

    public void CreateLabel(GameObject part, string labelText)
    {
        GameObject labelInstance = Instantiate(labelPrefab, canvas.transform);
        labelInstance.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = labelText; // Set the text of the label
        Vector2 partScreenLocation = Camera.main.WorldToScreenPoint(part.transform.position);
        partScreenLocation.x += 100;
        partScreenLocation.y += 100;
        labelInstance.GetComponent<RectTransform>().anchoredPosition = partScreenLocation;
        currentLabel = labelInstance;
    }
    
    public void destroyLabel()
    {
        if (currentLabel != null)
        {
            Destroy(currentLabel);
        }
    }
   
}
