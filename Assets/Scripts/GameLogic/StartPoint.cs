using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SB.GameLogic
{
    public class StartPoint : PointBase
    {
        private IEnumerator Start()
        {
            while (GameManager.Instance == null)
                yield return null;

            while (GameManager.Instance.CurrentPlayer == null)
                yield return null;

            GameManager.Instance.CurrentPlayer.transform.position = transform.position;
        }
    }
}
