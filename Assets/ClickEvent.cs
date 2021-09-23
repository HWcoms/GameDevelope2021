//현재 클릭 시 이름으로 오브젝트를 찾는 소스

using UnityEngine;
using System.Collections;

public class ClickEvent : MonoBehaviour {
    public int ObstacleNumber = 0;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {//마우스 좌측 클릭 발생 시.
            GameObject tempObj = null;//임시 오브젝트 생성.
            if (ObstacleNumber > 0 && ObstacleNumber < 6)
            {
                tempObj = GameObject.Find("Obstacle" + ObstacleNumber.ToString());
                if (tempObj != null)
                {//게임오브젝트를 성공적으로 받았다면.
                    Debug.Log("성공적으로 " + tempObj.name + " 오브젝트를 받았습니다.");
                }
                else
                {
                    Debug.LogError(ObstacleNumber.ToString() + "번 몬스터를 얻는데 실패했습니다.");
                }
            }
            else
            {
                Debug.LogError("잘못된 몬스터 번호입니다.");
            }

        }
    }
}



