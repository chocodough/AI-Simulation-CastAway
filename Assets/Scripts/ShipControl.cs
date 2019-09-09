using UnityEngine;
using System.Collections;

public class ShipControl : MonoBehaviour {

    public NavMeshAgent nvAgent;
    public Transform ShipDestinatnion;
    public bool GoToIsland = false;

	// Use this for initialization
	void Start () {
        nvAgent = GetComponent<NavMeshAgent>();
        ShipDestinatnion = GameObject.Find("ShipDestination").GetComponent<Transform>();
        nvAgent.destination = GameObject.Find("ShipDestination").GetComponent<Transform>().position;
    }
	
	// Update is called once per frame
	void Update () {
        if (GetComponent<Transform>().position.x == ShipDestinatnion.position.x
            && GetComponent<Transform>().position.z == ShipDestinatnion.position.z)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter(Collider coll)
    {
        if(coll.tag == "sos")
        {
            GoToIsland = true;
            nvAgent.destination = GameObject.Find("SavePoint").GetComponent<Transform>().position;
        }
    }
}
