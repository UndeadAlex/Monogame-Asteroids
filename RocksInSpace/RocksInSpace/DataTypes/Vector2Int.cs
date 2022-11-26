using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Design;

namespace RocksInSpace.DataTypes
{
    public struct Vector2Int : IEquatable<Vector2Int>
    {

        private static readonly Vector2Int zeroVector = new Vector2Int(0, 0);
        private static readonly Vector2Int unitVector = new Vector2Int(1, 1);
        private static readonly Vector2Int unitXVector = new Vector2Int(1, 0);
        private static readonly Vector2Int unitYVector = new Vector2Int(0, 1);

        public int X { get; private set; }
        public int Y { get; private set; }

        public static Vector2Int Zero => zeroVector;
        public static Vector2Int One => unitVector;
        public static Vector2Int Right => unitXVector;
        public static Vector2Int Left => -unitXVector;
        public static Vector2Int Down => unitYVector;
        public static Vector2Int Up => -unitYVector;

        public Vector2Int(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2Int(int value)
        {
            this.X = this.Y = value;
        }

        public static Vector2Int operator -(Vector2Int value)
        {
            value.X = 0 - value.X;
            value.Y = 0 - value.Y;
            return value;
        }

        public static Vector2Int operator +(Vector2Int value1, Vector2Int value2)
        {
            value1.X += value2.X;
            value1.Y += value2.Y;
            return value1;
        }
        public static Vector2Int operator -(Vector2Int value1, Vector2Int value2)
        {
            value1.X -= value2.X;
            value1.Y -= value2.Y;
            return value1;
        }

        public static Vector2Int operator *(Vector2Int value1, Vector2Int value2)
        {
            value1.X *= value2.X;
            value1.Y *= value2.Y;
            return value1;
        }

        public static Vector2Int operator *(Vector2Int value, int scaleFactor)
        {
            value.X *= scaleFactor;
            value.Y *= scaleFactor;
            return value;
        }

        public static Vector2Int operator *(float scaleFactor, Vector2Int value)
        {
            value.X = (int)(value.X * scaleFactor);
            value.Y = (int)(value.Y * scaleFactor);
            return value;
        }

        public static Vector2Int operator /(Vector2Int value1, Vector2Int value2)
        {
            value1.X /= value2.X;
            value1.Y /= value2.Y;
            return value1;
        }

        public static Vector2Int operator /(Vector2Int value1, float divider)
        {
            float num = 1f / divider;
            value1.X = (int)(value1.X * num);
            value1.Y = (int)(value1.Y * num);
            return value1;
        }

        public static bool operator ==(Vector2Int value1, Vector2Int value2)
        {
            if (value1.X == value2.X)
            {
                return value1.Y == value2.Y;
            }

            return false;
        }

        public static bool operator !=(Vector2Int value1, Vector2Int value2)
        {
            if (value1.X == value2.X)
            {
                return value1.Y != value2.Y;
            }

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is Vector2Int)
            {
                return Equals((Vector2Int)obj);
            }

            return false;
        }

        public bool Equals(Vector2Int other)
        {
            if (X == other.X)
            {
                return Y == other.Y;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return (X.GetHashCode() * 397) ^ Y.GetHashCode();
        }

        public override string ToString()
        {
            return "{X:" + X + " Y:" + Y + "}";
        }
    }
}
