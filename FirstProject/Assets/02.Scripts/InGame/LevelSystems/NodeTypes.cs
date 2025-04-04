using System;

namespace Match3.InGame.LevelSystems
{
    [Flags]
    public enum NodeTypes
    {
        Nothing     = 0 << 0,
        BasicRed    = 1 << 0, // 0001
        BasicOrange = 1 << 1, // 0010
        BasicYellow = 1 << 2, // 0100
        BasicGreen  = 1 << 3,
        BasicBlue   = 1 << 4,
        BasicPurple = 1 << 5,
    }

}