using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ClickType
{
    NoClick = 0,
    left ,
    middle,
    right
}
class ClickInfo
{
    public RaycastHit hitinfo;
    public ClickType clickType;

    public ClickInfo()
    {
        clickType = ClickType.NoClick;
    }
    public void GetInfo(Ray ray,out  RaycastHit hit,int key)
    {
        if (Physics.Raycast(ray, out hit))
        {
            hitinfo = hit;
            switch (key)
            {
                case 0:clickType = ClickType.left;break;
                case 1:clickType = ClickType.right;break;
                case 2:clickType = ClickType.middle;break;
                default:clickType = ClickType.NoClick;break;
            }
        }
    }
}
public class PlayerControl : MonoBehaviour
{
    private LinkedList<ShipEntity> ControlShips;
    
    void Start()
    {
        ControlShips = new LinkedList<ShipEntity>();
    }

    // Update is called once per frame
    void Update()
    {
        //监听用户输入
        ClickInfo clickInfo = GetPlayerInput();
        if(clickInfo.clickType > 0)
        {
            LogUserInput(clickInfo);
            //处理用户输入
            DealWithPlayerInput(clickInfo);
            //将事件分发到对应的位置
        }

    }
    private ClickInfo GetPlayerInput()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        ClickInfo clickInfo = new ClickInfo();
        //处理鼠标点击事件
        if (Input.GetMouseButtonDown(0)){
            clickInfo.GetInfo(ray, out hit,0);
        }
        if (Input.GetMouseButtonDown(2))
        {
            clickInfo.GetInfo(ray, out hit,2);
        }
        if(Input.GetMouseButtonDown(1))
        {
            clickInfo.GetInfo(ray, out hit, 1);
        }
        return clickInfo;
    }
    private void DealWithPlayerInput(ClickInfo clickInfo)
    {
        if (clickInfo.clickType == ClickType.left)
        {
            //点击选择船
            ShipEntity ship = GameObject.Find(clickInfo.hitinfo.transform.name).GetComponent<ShipEntity>();
            //如果这条船已经被选中了，那就取消选中，否则就添加选中
            if (ControlShips.Contains(ship))
            {
                ControlShips.Remove(ship);
                ship.ChooseControl(false);
            }
            else
            {
                // 清除掉当前列表中的控制船队，添加新的船
                foreach(ShipEntity shipEntity in ControlShips)
                {
                    ship.ChooseControl(false);
                }
                ControlShips.Clear();
                ControlShips.AddLast(ship);
                ship.ChooseControl(true);
            }
        }
        if(clickInfo.clickType == ClickType.right)
        {
            if(clickInfo.hitinfo.transform.name == "light_startfiled")
            {
                foreach(ShipEntity shipEntity in ControlShips)
                {
                    shipEntity.setTarget(clickInfo.hitinfo.point);
                }
            }
        }
    }
    private void LogUserInput(ClickInfo clickInfo)
    {
        if(clickInfo != null)
        {
            Debug.Log(clickInfo.hitinfo.transform.name);
            Debug.Log(clickInfo.clickType);
        }

    }
}
