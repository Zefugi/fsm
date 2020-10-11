using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zefugi.FinitiStateMachine
{
    public class ExplicitState
    {
        public FiniteStateMachine StateMachine { get; internal set; }

        public virtual ExplicitState Update() => null;

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}
