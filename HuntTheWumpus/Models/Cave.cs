﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HuntTheWumpus.Models
{
    public class Cave
    {
        public List<Room> Rooms { get; set; } = new List<Room>();
        public Dictionary<int, List<int>> RoomNeighbors { get; set; }
        public Dictionary<int, List<int>> RoomConnections { get; set; }
        public int Number { get; set; }
        public Cave(int caveNumber)
        {
            Number = caveNumber;
            for (var i = 1; i < 31; i++)
            {
                var room = new Room(i);
                Rooms.Add(room);
            }
            RoomNeighbors = new Dictionary<int, List<int>>();
            SetNeighbors();

            RoomConnections = new Dictionary<int, List<int>>();
            SetConnections();

        }
        public void SetNeighbors()
        {
            //TODO: write an algorithm to figure out how rooms should be connected
            RoomNeighbors = GetRoomDataFromFile("DataFiles/CaveNeighbors.txt");
        }
        public List<Room> GetNeighbors(int index)
        {
            //TODO: Figure out if there is a better way to do this
            var roomNumbers = new List<int>();
            roomNumbers.AddRange(RoomNeighbors[index]);

            var neighbors = new List<Room>();
            foreach (var room in Rooms)
                if (roomNumbers.Contains(room.Name))
                    neighbors.Add(room);

            return neighbors;
        }
        public void SetConnections()
        {
            RoomConnections = GetRoomDataFromFile("DataFiles/Cave" + Number + ".txt");
        }
        public List<Room> GetConnections(int index)
        {
            var roomNumbers = new List<int>();
            roomNumbers.AddRange(RoomConnections[index]);

            var connections = new List<Room>();
            foreach (var room in Rooms)
                if (roomNumbers.Contains(room.Name))
                    connections.Add(room);

            return connections;
        }

        public List<int> GetRoomNumbers(List<Room> rooms)
        {
            var roomNumber = new List<int>();
            foreach (var room in rooms)
                roomNumber.Add(room.Name);

            return roomNumber;
        }
        private Dictionary<int,List<int>> GetRoomDataFromFile(string path)
        {
            var roomData = new Dictionary<int, List<int>>();
            using (var streamReader = new StreamReader(path))
            {
                var i = 1;
                while (!streamReader.EndOfStream)
                {
                    roomData.Add(i, streamReader.ReadLine().Split(',').Select(int.Parse).ToList());
                    i++;
                }

            }
            return roomData;
        }
    }
}