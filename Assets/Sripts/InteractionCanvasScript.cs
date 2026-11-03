using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
public class InteractionCanvasScript : MonoBehaviour
{
    [SerializeField] private GameObject canvas;

    private void Awake()
    {
        Hide();
    }

    public void Show()
    {
        canvas.SetActive(true);
    }

    public void Hide()
    {
        canvas.SetActive(false);
    }
}


