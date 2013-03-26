using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Bridge.Domain
{
    public class PlayerPosition : IEquatable<PlayerPosition>
    {
        public static PlayerPosition North = new PlayerPosition(0, "North",1);
        public static PlayerPosition East = new PlayerPosition(1, "East",2);
        public static PlayerPosition South = new PlayerPosition(2, "South",3);
        public static PlayerPosition West = new PlayerPosition(3, "West",0);
        public static List<PlayerPosition> Players = new List<PlayerPosition>() { North, East, South, West };


        public int Order { get; private set; }

        public int PbnIndex { get; private set; }

        public string FullName { get; private set; }

        public string FirstLetter { get; private set; }

        public PlayerPosition(int order)
            : this(Players.Single(x => x.Order == order))
        {

        }

        public PlayerPosition(string letter)
            : this(Players.Single(x => x.FirstLetter == letter))
        {

        }

        public PlayerPosition(char letter)
            : this(Players.Single(x => x.FirstLetter[0] == letter))
        {

        }

        internal PlayerPosition(PlayerPosition playerPosition)
            : this(playerPosition.Order, playerPosition.FullName,playerPosition.PbnIndex)
        {
        }

        internal PlayerPosition(int order, string fullName, int pbnIndex)
        {
            Order = order;
            PbnIndex = pbnIndex;
            FullName = fullName;
            FirstLetter = fullName[0].ToString(CultureInfo.InvariantCulture).ToUpper();
        }

        public bool Equals(PlayerPosition other)
        {
            return Order == other.Order;
        }

        public static bool operator ==(PlayerPosition x, PlayerPosition y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(PlayerPosition x, PlayerPosition y)
        {
            return !(x == y);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((PlayerPosition)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Order;
                hashCode = (hashCode * 397) ^ (FullName != null ? FullName.GetHashCode() : 0);
                return hashCode;
            }
        }

        public override string ToString()
        {
            return FullName;
        }
    }
}
