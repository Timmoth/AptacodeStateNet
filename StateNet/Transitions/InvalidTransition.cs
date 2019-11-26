﻿using Aptacode.StateNet.Exceptions;
using Aptacode.StateNet.Inputs;
using Aptacode.StateNet.States;
using System;

namespace Aptacode.StateNet.Transitions
{
    public class InvalidTransition : BaseTransition
    {
        /// <summary>
        /// A transition which CAN NOT exist
        /// </summary>
        /// <param name="state"></param>
        /// <param name="input"></param>
        /// <param name="message"></param>
        public InvalidTransition(State state, Input input, string message) : base(state, input, message) { }

        /// <summary>
        /// Throws an exception as an invalidTransition cannot be applied.
        /// </summary>
        /// <returns></returns>
        public override State Apply() => throw new InvalidTransitionException(Origin, Input);

        public override string ToString() => $"Invalid Transition: {Origin}({Input})";
    }
}
