using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ShipScout : MonoBehaviour
{
    // 战舰侦察导航系统
    private Ray ray;
    private RaycastHit hit;
    private List<ShipEntity> EnemyList;
    private int timeWait;
    void Start()
    {
        EnemyList = new List<ShipEntity>();
        timeWait = 360;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeWait%60==0)
        {
            EnemyList.Clear();
            ScoutSystem();
        }
        if(timeWait == 360)
        {
            Vector3 nextPosition = NavigationSystem();
            gameObject.GetComponent<ShipEngineRoom>().SetEndPosition(nextPosition);
            timeWait = 0;
        }
        else
        {
            timeWait += 1;
        }
        
    }

    // 侦察系统
    void ScoutSystem()
    {
        float viewDistance = gameObject.GetComponent<ShipInfo>().ViewDistance;
        for(int i = 0; i < 360; i++)
        {
            float rad = (float)(i * (Mathf.PI / 180.0));
            float x = Mathf.Cos(rad) * viewDistance;
            float z = Mathf.Sin(rad) * viewDistance;
            ray = new Ray(gameObject.transform.position, new Vector3(x, gameObject.transform.position.y, z));
            if(Physics.Raycast(ray, out hit))
            {
                if(hit.transform.tag == "Ship")
                {
                    //调用敌我识别功能
                    var shipInfo = gameObject.GetComponent<ShipInfo>();
                    if (shipInfo.IsEnemy(hit.transform.GetComponent<ShipInfo>().BeLong))
                    {
                        //如果是敌人则加入导航选择系统
                        EnemyList.Add(hit.transform.GetComponent<ShipEntity>());
                    }
                }
            }
        }
    }
    // 导航系统
    Vector3 NavigationSystem()
    {
        if(EnemyList.Count > 0)
        {
            // 取当前列表第一个作为敌人
            return EnemyList.First().GetComponent<Transform>().position;
        }
        else
        {
            return new Vector3(gameObject.transform.position.x + 100 * Random.Range(1, 2) * Random.Range(-1,1),
                gameObject.transform.position.y,
                gameObject.transform.position.z + 100 * Random.Range(1, 2) * Random.Range(-1, 1));
        }
    }
}
