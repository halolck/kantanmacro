using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macroproject
{
    public class undoredo
    {
        #region delegate
        public delegate void RecordableAction();
        #endregion

        #region internal

        internal class State
        {
            public State PrevState { get; set; }
            public State NextState { get; set; }
            public Command PrevCommand { get; set; }
            public Command NextCommand { get; set; }
        }

        internal class Command
        {

            public Command()
            { }

            public Command(RecordableAction act, RecordableAction undo)
            {
                _Do = act;
                _Undo = undo;
            }

            public void executeAction()
            {
                _Do?.Invoke();
            }

            public void undoAction()
            {
                _Undo?.Invoke();
            }

            protected event RecordableAction _Do;
            protected event RecordableAction _Undo;
        }

        internal class TransactionCommand : Command
        {
            public TransactionCommand()
            {
                _Do += () =>
                {
                    for (int i = 0; i < CommandSet.Count; ++i)
                    {
                        CommandSet[i].executeAction();
                    }
                };
                _Undo += () =>
                {
                    for (int i = CommandSet.Count - 1; i >= 0; --i)
                    {
                        CommandSet[i].undoAction();
                    }
                };
            }

            public void AddCommand(Command cmd)
            {
                if (cmd == null)
                {
                    Console.WriteLine("Cannot add null command");
                    return;
                }
                CommandSet.Add(cmd);
            }

            List<Command> CommandSet = new List<Command>();
        }
        #endregion

        #region static変数

        static Object Lock = new Object();
        static List<State> States = new List<State>();
        static TransactionCommand TransCommand;

        static State CurrentState
        {
            get
            {
                // doesn't execute normmaly
                if (States.Count <= 0)
                    return null;

                // doesn't execute normmaly
                if (!IsInRange(0, States.Count - 1, CurrentStateIndex))
                    return null;

                var index = Clamp(0, States.Count - 1, CurrentStateIndex);
                return States.ElementAt(index);
            }
        }

        static int CurrentStateIndex
        {
            get
            {
                return _CurrentStateIndex;
            }
            set
            {
                if (_CurrentStateIndex != value)
                    _CurrentStateIndex = value;
            }
        }
        static int _CurrentStateIndex = 0;

        static bool Transacting
        {
            get
            {
                return _Transacting;
            }
            set
            {
                if (_Transacting != value)
                    _Transacting = value;
            }
        }
        static bool _Transacting = false;


        public static void Record(RecordableAction act, RecordableAction undo)
        {
            lock (Lock)
            {
                var cmd = new Command(act, undo);
                cmd.executeAction();
                if (Transacting)
                {
                    TransCommand.AddCommand(cmd);
                }
                else
                {
                    var newState = new State();
                    newState.PrevCommand = cmd;
                    AddNewState(newState);
                }
            }
        }

        public static void Undo()
        {
            lock (Lock)
            {
                var prev = CurrentState.PrevState;
                if (prev == null)
                {
                    Console.WriteLine("Invalid operation.");
                    return;
                }

                CurrentState.PrevCommand?.undoAction();
                CurrentStateIndex = States.IndexOf(prev);
            }
        }
        public static void Redo()
        {
            lock (Lock)
            {
                var next = CurrentState.NextState;
                if (next == null)
                {
                    Console.WriteLine("Invalid operation.");
                    return;
                }

                CurrentState.NextCommand?.executeAction();
                CurrentStateIndex = States.IndexOf(next);
            }
        }

        static public void BeginTransaction()
        {
            lock (Lock)
            {
                TransCommand = new TransactionCommand();
                Transacting = true;
            }
        }

        static public void EndTransaction()
        {
            lock (Lock)
            {
                Transacting = false;
                var newState = new State();
                newState.PrevCommand = TransCommand;
                AddNewState(newState);
                TransCommand = null;
            }
        }

        static undoredo()
        {
            States.Add(new State());
        }

        static void EliminateStates(State begin)
        {
            if (begin == null)
                return;

            var beginIndex = States.IndexOf(begin);
            if (beginIndex <= 0)
                return;

            var removeCount = States.Count - beginIndex;
            States.RemoveRange(beginIndex, removeCount);
        }

        static void AddNewState(State newState)
        {
            var eliminateState = CurrentState?.NextState;
            if (eliminateState != null)
                EliminateStates(eliminateState);

            CurrentState.NextState = newState;
            CurrentState.NextCommand = newState.PrevCommand;
            newState.PrevState = CurrentState;
            States.Add(newState);
            CurrentStateIndex = States.IndexOf(newState);
        }

        static bool IsInRange(int min, int max, int value)
        {
            return min <= value && value <= max;
        }

        static int Clamp(int min, int max, int value)
        {
            if (min > value)
                return min;
            if (max < value)
                return max;

            return value;
        }
        #endregion

    }
}
