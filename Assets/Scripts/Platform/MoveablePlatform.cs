using Unity.Collections;
using UnityEngine;

namespace Script.Platform
{
    public class MoveablePlatform : MonoBehaviour
    {
        public MoveablePlatformData Data;

        [ReadOnly] [SerializeField] private int currentIndex;
        [ReadOnly] [SerializeField] private float moveTimer;
        [ReadOnly] [SerializeField] private float waitTimer;

        private void FixedUpdate()
        {
            if (Data == null || Data.DataList.Count == 0)
            {
                return;
            }

            var platformMoveDataList = Data.DataList;
            var platformMoveData = platformMoveDataList[currentIndex];

            if (moveTimer >= platformMoveData.MoveTime)
            {
                if (waitTimer >= platformMoveData.WaitTime)
                {
                    moveTimer = 0;
                    waitTimer = 0;
                    currentIndex++;

                    if (currentIndex >= platformMoveDataList.Count)
                    {
                        currentIndex = 0;
                    }
                }

                waitTimer += Time.deltaTime;
                return;
            }

        
            var speed = platformMoveData.Speed * Time.deltaTime;

            if (platformMoveData.useTarget)
            {
                transform.position = Vector2.MoveTowards(transform.position,
                    platformMoveData.TargetPosition, speed);
            }
            else
            {
                transform.position += platformMoveData.Direction * speed;
            }

            moveTimer += Time.deltaTime;
        }
    }
}