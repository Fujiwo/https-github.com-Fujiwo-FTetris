using System;

namespace FTetris.Model
{
    public class StatefulCell
    {
        public event Action<StatefulCell, int> StateChanged;

        int stateIndex = 0;

        public int StateIndex {
            get { return stateIndex; }
            set {
                if (value != stateIndex) {
                    stateIndex = value;
                    StateChanged?.Invoke(this, stateIndex);
                }
            }
        }
    }
}
