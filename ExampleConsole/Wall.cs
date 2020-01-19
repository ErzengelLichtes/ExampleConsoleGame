﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExampleConsole
{
    public enum Direction
    {
        Horizontal,
        Vertical
    }

    class Wall : IBlock
    {
        public Wall(int x, int y
                   , int length, Direction direction
                  , string display
        )
        {
            X = x;
            Y = y;
            Length = length;
            Direction = direction;
            Display = display;
            switch (direction)
            {
            case Direction.Horizontal:
                Width = length;
                Height = 1;
                break;
            case Direction.Vertical:
                Height = length;
                Width = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(direction), direction, message: null);
            }
        }

        /// <inheritdoc />
        public int X { get; }

        /// <inheritdoc />
        public int Y { get; }

        public int Length { get; }
        public Direction Direction { get; }

        /// <inheritdoc />
        public string Display { get; }

        /// <inheritdoc />
        public int Width { get; }

        /// <inheritdoc />
        public int Height { get; }
    }
}
