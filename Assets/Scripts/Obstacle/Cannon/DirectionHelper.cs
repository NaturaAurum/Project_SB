using UnityEngine;

namespace Script.Obstacle.Cannon
{
    public static class DirectionHelper
    {
        public static Vector2 DirectionToVector(Direction dir)
        {
            return dir switch
            {
                Direction.Right => Vector2.right,
                Direction.Left => Vector2.left,
                Direction.Up => Vector2.up,
                Direction.Down => Vector2.down,
                _ => Vector2.left
            };
        }
    }
}