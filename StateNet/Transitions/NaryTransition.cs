﻿using System;
using System.Collections.Generic;
using Aptacode.StateNet.Exceptions;
using Aptacode.StateNet.TransitionResult;

namespace Aptacode.StateNet.Transitions
{
    public class NaryTransition : ValidTransition
    {
        /// <summary>
        ///     Defines a transition to the 'nextState' when the 'input' is applied to the current state
        /// </summary>
        /// <param name="state"></param>
        /// <param name="input"></param>
        /// <param name="nextState"></param>
        /// <param name="acceptanceCallback"></param>
        /// <param name="message"></param>
        public NaryTransition(string state, string input, List<string> nextStates,
            Func<NaryTransitionResult> acceptanceCallback, string message) : base(state, input, message)
        {
            NextStates = nextStates;
            AcceptanceCallback = acceptanceCallback;
        }

        /// <summary>
        ///     The output state of the unary transition
        /// </summary>
        public List<string> NextStates { get; }

        protected Func<NaryTransitionResult> AcceptanceCallback { get; set; }

        /// <summary>
        ///     Apply the transition
        /// </summary>
        /// <returns></returns>
        public override string Apply()
        {
            var result = AcceptanceCallback?.Invoke();
            if (result == null || !result.Success || !NextStates.Contains(result.Choice))
            {
                throw new AcceptanceCallbackFailedException(State, Input);
            }

            return result.Choice;
        }

        public override string ToString()
        {
            return $@"Unary Transition: {State}({Input})->{{ {string.Join("| ", NextStates)} }}";
        }
    }
}