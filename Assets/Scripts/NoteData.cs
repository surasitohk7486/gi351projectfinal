using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NoteData", menuName = "Note/NoteData", order = 1)]
public class NoteData : ScriptableObject
{
    public string noteTitle; // ชื่อโน้ต
    public string noteContent; // เนื้อหา
    public GameObject noteUIPrefab; // Prefab UI เฉพาะของโน้ต
}
