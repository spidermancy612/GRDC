using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{

    public enum TurnType
    {
        Move,
        WeakMove,
        StrongMove,
        Attack,
        Shield,
        None
    }

    public enum MoveVector
    {
        Front,
        Back,
        Left, 
        Right,
        TurnLeft,
        TurnRight
    }

    public class Action
    {
        public TurnType turnType;
        public Dictionary<MoveVector, int> moveVector;

        public Action(TurnType turnType, Dictionary<MoveVector, int> moveVector)
        {
            this.turnType = turnType;
            this.moveVector = moveVector;
        }

        public Action(TurnType turnType)
        {
            this.turnType = turnType;
            this.moveVector = null;
        }
    }
}
