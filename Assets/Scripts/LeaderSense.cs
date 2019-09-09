using UnityEngine;
using System.Collections;

public class LeaderSense : MonoBehaviour
{
    public GameObject Leader;
   
    void Start()
    {
        Leader = GameObject.Find("Leader");  // 리더 객체를 가져옴
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.tag == "grass" )
        {
            print("grass!!");
            Leader.GetComponent<LeaderControl>().FoundGrass = true;
            Leader.GetComponent<LeaderControl>().Target = coll.gameObject;
        }
    }


}
