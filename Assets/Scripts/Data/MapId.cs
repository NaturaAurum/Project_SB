using SB.Data.Enums;
using Sirenix.OdinInspector;

namespace SB.Data
{
    [System.Serializable]
    public struct MapId
    {
        // TODO
        public static MapId None => By(0,CategoryType.None);

        [BoxGroup("Map ID")]
        public int Id;
        [BoxGroup("Map ID")]
        public CategoryType Category;

        public static MapId By(
            int id,
            CategoryType theme
        )
        {
            var mapId = new MapId();
            mapId.Id = id;
            mapId.Category = theme;
            return mapId;
        }

        public override readonly string ToString()
        {
            return $"Map.{Category}.{Id}";
        }

        public static bool operator ==(MapId id1, MapId id2)
        {
            var sameId = id1.Id == id2.Id;
            var sameTheme = id1.Category == id2.Category;
            return sameId && sameTheme;
        }

        public static bool operator !=(MapId id1, MapId id2) => !(id1 == id2);
    }
}