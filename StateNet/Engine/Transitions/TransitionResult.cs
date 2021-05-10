namespace Aptacode.StateNet.Engine.Transitions
{
    public record TransitionResult(string Message, bool Success, Transition? Transition)
    {
        public static TransitionResult Fail(string message)
        {
            return new(message, false, null);
        }

        public static TransitionResult Ok(Transition transition, string message)
        {
            return new(message, true, transition);
        }
    }
}