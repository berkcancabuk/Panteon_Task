using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollControl : MonoBehaviour
{
    public ScrollRect scrollRect;
    public float verticalNormalizedDownThresholdValue = 0;
    public float verticalNormalizedUpThresholdValue = 1;
    public int leftBoxId, rightBoxId;
    private bool _isUpOrDown;
    private bool _isUp;
    private bool _isDown;
    [SerializeField] private List<RectTransform> contenChildtList = new List<RectTransform>();

    private void Start()
    {
        
    }
    private void Update()
    {

    }

    public void Scroll()
    {
        ScrollController();
    }
    public void FirstPlaceEntered(int leftBoxId,int rightBoxId,bool isUpOrDown)
    {
       
            this.leftBoxId = leftBoxId;
            this.rightBoxId = rightBoxId;
            _isUpOrDown = isUpOrDown;
    }
    public void ChangeContentChildPos(int LeftBoxGO, int RightBoxGO,int distanceY)
    {
        contenChildtList[LeftBoxGO].anchoredPosition += new Vector2(0, distanceY);
        contenChildtList[RightBoxGO].anchoredPosition += new Vector2(0, distanceY);
    }
    public void ScrollController()
    {
        if (scrollRect.verticalNormalizedPosition <= verticalNormalizedDownThresholdValue)
        {
            _isUp = true;
            if (_isDown)
            {
                if (leftBoxId == contenChildtList.Count-2)
                {
                    leftBoxId = 0;
                    rightBoxId = 1;
                }
                else
                {
                    leftBoxId += 2;
                    rightBoxId += 2;
                }
                _isDown = false;
            }
            if (!_isUpOrDown)
            {
                FirstPlaceEntered(0, 1, true);
            }
            ChangeContentChildPos(leftBoxId, rightBoxId, -2400);

            if (leftBoxId == contenChildtList.Count - 2)
            {
                leftBoxId = 0;
                rightBoxId = 1;
                verticalNormalizedDownThresholdValue -= .25f;
            }
            else
            {
                leftBoxId += 2;
                rightBoxId += 2;
                verticalNormalizedDownThresholdValue -= .25f;

            }
            verticalNormalizedUpThresholdValue = verticalNormalizedDownThresholdValue + .25f;
        }
        else if (scrollRect.verticalNormalizedPosition > verticalNormalizedUpThresholdValue)
        {
            _isDown = true;
            if (_isUp)
            {
                if (leftBoxId == 0)
                {
                    leftBoxId = 10;
                    rightBoxId = 11;
                }
                else
                {
                    leftBoxId -= 2;
                    rightBoxId -= 2;
                }
                _isUp = false;
            }
            if (!_isUpOrDown)
            {
                FirstPlaceEntered(contenChildtList.Count - 2, contenChildtList.Count - 1, true);
            }
            ChangeContentChildPos(leftBoxId, rightBoxId, +2400);
            if (leftBoxId == 0)
            {
                leftBoxId = 10;
                rightBoxId = 11;
                verticalNormalizedUpThresholdValue += .25f;
            }
            else
            {
                leftBoxId -= 2;
                rightBoxId -= 2;
                verticalNormalizedUpThresholdValue += .25f;
            }
            verticalNormalizedDownThresholdValue = verticalNormalizedUpThresholdValue - .25f;
        }
    }
}