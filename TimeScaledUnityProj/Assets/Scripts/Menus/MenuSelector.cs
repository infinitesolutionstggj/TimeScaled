using UnityEngine;
using System.Collections;

public class MenuSelector : MonoBehaviour
{
    public bool SelectionConfirmed { get; set; }

    void Awake()
    {
        SelectionConfirmed = false;
    }

	void Start () 
	{

	}

	void Update () 
	{
        if (SelectionConfirmed)
        {
            GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }
        else
        {
            GetComponentInChildren<ParticleSystem>().enableEmission = false;
        }
	}
}
