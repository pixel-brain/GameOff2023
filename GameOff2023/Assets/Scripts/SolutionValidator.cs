using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolutionValidator : NodeScript
{
    [Header("Melody Settings")]
    [SerializeField] NoteListWrapper[] melodyNotes; // custom dictionary for pair notes with beats
    public GameObject notePrefab;

    [Header("Display Settings")]
    public Vector2 margin;

    private RectTransform rectTransform;
    private List<Note>[] answerNotes;


    protected override void Start()
    {
        base.Start();
        rectTransform = GetComponent<RectTransform>();
        SpawnNotes();
    }

    protected override void Beat()
    {
        base.Beat();

    }

    // returns true if input note is valid
    public bool InputNote(Note n, int beat)
    {
        answerNotes[beat].Add(n);
        return melodyNotes[beat].noteList.Contains(n);
    }

    private void SpawnNotes()
    {
        Vector2 spawnPos = new Vector2(-rectTransform.sizeDelta.x * 0.5f + margin.x, 0);
        float xOffset = (rectTransform.sizeDelta.x - margin.x * 2) / melodyNotes.Length;
        float yOffset = rectTransform.sizeDelta.y * 0.5f - margin.y;

        for (int i = 0; i < melodyNotes.Length; i++)
        {
            if (melodyNotes[i].noteList.Count > 1)
                spawnPos.y = -yOffset;
            else
                spawnPos.y = 0;
            foreach(Note note in melodyNotes[i].noteList)
            {
                GameObject spawn = Instantiate(notePrefab, spawnPos, Quaternion.identity);

                spawn.transform.SetParent(this.transform);
                spawn.GetComponent<RectTransform>().localPosition = spawnPos;
                spawn.transform.localScale = Vector3.one;
                spawnPos.y = yOffset;
                spawn.GetComponent<NoteDisplay>().SetNote(note, true);
            }
            spawnPos.x += xOffset;
        }
    }
}


[System.Serializable]
public class NoteListWrapper
{
    public List<Note> noteList;
}
