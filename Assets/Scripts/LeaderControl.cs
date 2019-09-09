using UnityEngine;
using System.Collections;

public class LeaderControl : MonoBehaviour
{
    private NavMeshAgent nvAgent;

    public enum LeaderState {Wander,GoToGrass, WaitEating}  // 리더의 상태
    public LeaderState leaderstate = LeaderState.Wander;    // 상태를 Wander로 초기화

    public GameObject Target;

    public Transform[] p = new Transform[5];

    public bool FoundGrass = false;

    public float waitingTime = 0;
    
    void LeaderAction()   // FSM 상태별 동작
    {
        switch (leaderstate)
        {
            case LeaderState.Wander:
                if (GetComponent<Transform>().position.x == nvAgent.destination.x
                    && GetComponent<Transform>().position.z == nvAgent.destination.z)
                {
                    nvAgent.destination = p[Random.Range(0,4)].position;
                }
                break;
            case LeaderState.GoToGrass:
                nvAgent.destination = Target.GetComponent<Transform>().position;
                FoundGrass = false;
                break;
            case LeaderState.WaitEating:
                waitingTime += Time.deltaTime;
                if(waitingTime > 3)
                {
                    waitingTime = 0;
                    Destroy(Target);
                    nvAgent.destination = p[Random.Range(0, 4)].position;
                    leaderstate = LeaderState.Wander;                   
                }
                break;
            default:
                break;         
        }
    }

    void CheckLeaderState()  // FSM 상태간의 전환
    {
        if(leaderstate == LeaderState.Wander
            && FoundGrass)
        { 
            leaderstate = LeaderState.GoToGrass;
        }
        if(leaderstate == LeaderState.GoToGrass
            && GetComponent<Transform>().position.x == nvAgent.destination.x
            && GetComponent<Transform>().position.z == nvAgent.destination.z)
        {
            leaderstate = LeaderState.WaitEating;
        }
        if(leaderstate == LeaderState.WaitEating)
        {
            return;
        }
    }
  
    void Start()
    {
        for (int i = 0; i < 5; i++)
        {
            p[i] = GameObject.Find("p" + (i + 1).ToString()).GetComponent<Transform>();  //다섯 지점의 위치 저장
        }
        
        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
        nvAgent.destination = p[Random.Range(0, 4)].position;
    }

  
    void Update()
    {
        CheckLeaderState();
        LeaderAction();
    }


    
}
