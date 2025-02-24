using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller_Magnet : MonoBehaviour
{
    public float attractionRadius = 50f;
    private SphereCollider magnetCollider;
    public GameObject Player;

    void Start()
    {
        magnetCollider = gameObject.AddComponent<SphereCollider>();
        magnetCollider.isTrigger = true;
        magnetCollider.radius = attractionRadius;
        gameObject.tag = "Magnet"; // Tag para identificar el área
    }

    void Update()
    {
        // Sincroniza posición con el jugador
        transform.position = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);
    }
}
