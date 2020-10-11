using NUnit.Framework;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zefugi.FinitiStateMachine;
using NSubstitute.Extensions;

namespace Zefugi.FiniteStateMachine.Tests
{
    [TestFixture]
    public class FiniteStateMachine_Tests
    {
        [Test]
        public void CurrentState_IsSet_ByCtor()
        {
            var state = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            Assert.AreEqual(state, fsm.CurrentState);
        }

        [Test]
        public void EnterState_IsCalled_ByTransition_IfStateIsDifferen()
        {
            var state = new ExplicitState();
            var newState = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);
            
            bool enterStateWasCalled = false;
            fsm.EnterState += () => enterStateWasCalled = true;

            fsm.Transition(newState);

            Assert.IsTrue(enterStateWasCalled);
        }

        [Test]
        public void ExitState_IsCalled_ByTransition_IfStateIsDifferent()
        {
            var state = new ExplicitState();
            var newState = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            bool exitStateWasCalled = false;
            fsm.ExitState += () => exitStateWasCalled = true;

            fsm.Transition(newState);

            Assert.IsTrue(exitStateWasCalled);
        }

        [Test]
        public void ExplicitStateStateMachine_IsSet_ByCtor()
        {
            var state = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            Assert.AreEqual(fsm, state.StateMachine);
        }

        [Test]
        public void ExplicitStateStateMachine_IsSet_ByAdd()
        {
            var state = new ExplicitState();
            var newState = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            fsm.Add(newState);

            Assert.AreEqual(fsm, newState.StateMachine);
        }

        [Test]
        public void ExplicitState_IsAddedToStates_ByAdd()
        {
            var state = new ExplicitState();
            var newState = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            fsm.Add(newState);

            Assert.IsTrue(fsm.States.Contains(newState));
        }

        [Test]
        public void CurrentState_IsSetToInitialState_IfExplicitStateUpdateReturnsNull()
        {
            var state = new ExplicitState();
            var newState = new ExplicitState();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            fsm.Transition(newState);
            fsm.Update();

            Assert.AreEqual(state, fsm.CurrentState);
        }

        [Test]
        public void CurrentState_IsSetToNewState_IfExplicitStateUpdateReturnsState()
        {
            var newState = new ExplicitState();
            var state = Substitute.For<ExplicitState>();
            state.Update().Returns(newState);
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            fsm.Update();

            Assert.AreEqual(newState, fsm.CurrentState);
        }

        [Test]
        public void CurrentStateUpdate_IsCalled_ByUpdate()
        {
            var state = Substitute.For<ExplicitState>();
            var fsm = new FinitiStateMachine.FiniteStateMachine(state);

            fsm.Update();

            state.Received().Update();
        }

        [Test]
        public void CurrentStateOnEnter_IsCalled_ByTransition()
        {
            var initialState = new ExplicitState();
            var state = Substitute.For<ExplicitState>();
            var fsm = new FinitiStateMachine.FiniteStateMachine(initialState);

            fsm.Transition(state);

            state.Received().OnEnter();
        }

        [Test]
        public void CurrentStateOnExit_IsCalled_ByTransition()
        {
            var state = new ExplicitState();
            var initialState = Substitute.For<ExplicitState>();
            var fsm = new FinitiStateMachine.FiniteStateMachine(initialState);

            fsm.Transition(state);

            initialState.Received().OnExit();
        }

        [Test]
        public void CurrentStateOnEnter_IsCalled_ByCtor()
        {
            var initialState = Substitute.For<ExplicitState>();
            var fsm = new FinitiStateMachine.FiniteStateMachine(initialState);

            initialState.Received().OnEnter();
        }
    }
}
