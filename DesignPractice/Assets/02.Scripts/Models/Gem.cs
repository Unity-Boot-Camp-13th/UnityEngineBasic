using System;
using UnityEngine;

namespace DP.Models
{
    [Serializable]
    public struct Gem
    {
        public Gem(int value)
        {
            Value = value;
        }

        public int Value;
    }
}
