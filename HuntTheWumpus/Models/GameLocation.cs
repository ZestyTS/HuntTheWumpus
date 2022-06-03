using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HuntTheWumpus.Models.Hazard;

namespace HuntTheWumpus.Models
{
    internal class GameLocation
    {
        public int PlayerLocation { get; set; }
        public int WumpusLocation { get; set; }
        public int[] BatLocations { get; set; } = new int[2];
        public int[] PitfallLocations { get; set; } = new int[2];

        public GameLocation(List<Room> rooms)
        {
            var random = new Random();
            var hazardLocations = new List<int>();

            while (true)
            {
                var next = random.Next(2, rooms.Count);
                if (!hazardLocations.Contains(next))
                    hazardLocations.Add(next);

                if (hazardLocations.Count == 5)
                    break;
            }

            WumpusLocation = hazardLocations[0];
            BatLocations[0] = hazardLocations[1];
            BatLocations[1] = hazardLocations[2];
            PitfallLocations[0] = hazardLocations[3];
            PitfallLocations[1] = hazardLocations[4];

        }

        public bool DidArrowHitWumpus(int roomNumber)
        {
            return roomNumber == WumpusLocation;
        }
        private bool IsWumpusNearBy(List<Room> neighbors)
        {
            foreach (var neighbor in neighbors)
                if (neighbor.Wumpus)
                    return true;

            return false;
        }
        private bool IsBatNearBy(List<Room> neighbors)
        {
            foreach (var neighbor in neighbors)
                if (neighbor.Bat)
                    return true;

            return false;
        }
        private bool IsPitNearBy(List<Room> neighbors)
        {
            foreach (var neighbor in neighbors)
                if (neighbor.Pit)
                    return true;

            return false;
        }

        public string BuildWarningString(List<Room> neighbors, Bat bat, Pitfall pitfall, Wumpus wumpus)
        {
            var warning = "";
            if (IsBatNearBy(neighbors))
                warning += bat.Warning + "\n";
            if (IsPitNearBy(neighbors))
                warning += pitfall.Warning + "\n";
            if (IsWumpusNearBy(neighbors))
                warning += wumpus.Warning + "\n";

            return warning.TrimEnd('\n');
        }

        public string BuildNearByRooms(List<Room> rooms)
        {
            var roomNums = "";
            foreach (var room in rooms)
                roomNums += " " + room.Name + ",";
            roomNums = roomNums.TrimEnd(',');

            return roomNums;
        }
        public HazardEnum CheckIfRoomHasHazard(Room room)
        {
            if (room.Wumpus)
                return HazardEnum.Wumpus;
            if (room.Bat)
                return HazardEnum.Bat;
            if (room.Pit)
                return HazardEnum.Pitfall;

            return HazardEnum.Nothing;

        }

    }
}
