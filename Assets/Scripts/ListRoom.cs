using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ListRoom : MonoBehaviour
{
    private TemplateRooms templates;

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<TemplateRooms>();
        templates.rooms.Add(this.gameObject);
    }
}
