using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipEntity : MonoBehaviour
{
    // 玩家是否控制了这条船
    bool beControl;
    public string ShipName;
    public string BeLong;
    // 是否是玩家可以控制的船
    public bool Player;
    ShipEngineRoom engineRoom;
    ShipInfo shipInfo;
    void Start()
    {
        beControl = false;
        engineRoom = gameObject.AddComponent<ShipEngineRoom>();
        shipInfo = gameObject.AddComponent<ShipInfo>();
        if(ShipName == "")
        {
            ShipName = gameObject.transform.name;
        }
        shipInfo.InitInfomation(ShipName,BeLong,gameObject.GetComponent<NavMeshAgent>());
        InitBelongList();
        if (!Player)
            gameObject.AddComponent<ShipScout>();
        
    }

    void Update()
    {
        
    }

    private void InitBelongList()
    {
        switch (BeLong)
        {
            case "同盟":
                {
                    shipInfo.AddEnemy("帝国");
                }break;
            case "帝国":
                { shipInfo.AddEnemy("同盟"); }break;
        }
    }
    // 设置航行目标
    public void setTarget(Vector3 target)
    {
        if (target.y != gameObject.transform.position.y)
            target.y = gameObject.transform.position.y;
        engineRoom.SetEndPosition(target);
    }
    public void ChooseControl(bool control)
    {
        beControl = control;
        if (beControl)
        {
            Debug.Log(gameObject.transform.name + "被控制");
        }
        else
        {
            Debug.Log(gameObject.transform.name + "取消控制");
        }
    }
}
