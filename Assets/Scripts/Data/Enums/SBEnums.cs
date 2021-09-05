using System;

namespace SB.Data.Enums
{
    /// <summary>
    /// 카테고리
    /// </summary>
    public enum CategoryType
    {
        None,
        Mossy,
        Obstacles,
    }

    /// <summary>
    /// 어떤 종류의 블럭인지
    /// </summary>
    public enum BlockType
    {
        None,
        Background_Deco,
        Deco,
        Hanging,
        Hills,
        Platforms,
        TileSet,
        // TODO : 장애물 관련 Type들 추가하기
    }
}
