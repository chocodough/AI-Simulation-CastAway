using UnityEngine;
using System.Collections;

public class RabbitControl : MonoBehaviour {

    private NavMeshAgent nvAgent;
    public GameObject Leader;
    public Transform destination;

    void Start () {
        Leader = GameObject.Find("Leader");                // 리더 객체를 가져옴
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.speed = Random.Range(8,10);
        nvAgent.acceleration = Random.Range(9, 12);  // 속도 가속도를 토끼마다 다르게 부여
    }
    
    void Update() {
        nvAgent.destination = Leader.GetComponent<Transform>().position;
    }
}
