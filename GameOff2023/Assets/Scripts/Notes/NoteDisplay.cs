using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NoteDisplay : MonoBehaviour
{
    private Note note;
    [SerializeField] TextMeshProUGUI tmpText;
    [SerializeField] Image image;
    [SerializeField] RectTransform imageRectTransform;
    private float initialWidth;

    public Color32[] noteColors;


    private void Awake()
    {
        initialWidth = imageRectTransform.sizeDelta.x;
    }

    public void SetNote(Note n, bool leftAlign = false)
    {
        note = n;
        tmpText.text = note.type == NoteType.Rest ? "-" : ((int)note.type).ToString();
        image.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(initialWidth * (int)note.duration, imageRectTransform.sizeDelta.y);
        image.color = noteColors[(int)note.type];
        if (leftAlign)
            imageRectTransform.localPosition += Vector3.right * ((int)note.duration) * initialWidth * 0.5f;
    }
}
