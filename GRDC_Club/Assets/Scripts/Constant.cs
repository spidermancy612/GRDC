using NUnit.Framework.Constraints;

namespace Constants
{
    public static class Constant
    {
        public const int 
            WeakMovePoints = 2,
            MovePoints = 4,
            StrongMovePoints = 6;

        public const float 
            MoveOdd = 0.2857143f,
            WeakMoveOdd = 0.1428571f,
            StrongMoveOdd = 0.1904762f,
            AttackOdd = 0.2380952f,
            ShieldOdd = 0.1428571f;
    }
}