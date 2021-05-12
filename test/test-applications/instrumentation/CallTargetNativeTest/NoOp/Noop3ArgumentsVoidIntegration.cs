using System;
using Datadog.Trace.ClrProfiler.CallTarget;

namespace CallTargetNativeTest.NoOp
{
    /// <summary>
    /// NoOp Integration for 3 Arguments and Void Return
    /// </summary>
    public static class Noop3ArgumentsVoidIntegration
    {
        public static CallTargetState OnMethodBegin<TTarget, TArg1, TArg2, TArg3>(TTarget instance, TArg1 arg1, TArg2 arg2, TArg3 arg3)
        {
            CallTargetState returnValue = CallTargetState.GetDefault();
            Console.WriteLine($"ProfilerOK: BeginMethod(3)<{typeof(Noop3ArgumentsVoidIntegration)}, {typeof(TTarget)}, {typeof(TArg1)}, {typeof(TArg2)}, {typeof(TArg3)}>({instance}, {arg1}, {arg2}, {arg3})");
            if (instance?.GetType().Name.Contains("ThrowOnBegin") == true)
            {
                Console.WriteLine("Exception thrown.");
                throw new Exception();
            }

            return returnValue;
        }

        public static CallTargetReturn OnMethodEnd<TTarget>(TTarget instance, Exception exception, CallTargetState state)
        {
            CallTargetReturn returnValue = CallTargetReturn.GetDefault();
            Console.WriteLine($"ProfilerOK: EndMethod(0)<{typeof(Noop3ArgumentsVoidIntegration)}, {typeof(TTarget)}>({instance}, {exception?.ToString() ?? "(null)"}, {state})");
            if (instance?.GetType().Name.Contains("ThrowOnEnd") == true)
            {
                Console.WriteLine("Exception thrown.");
                throw new Exception();
            }

            return returnValue;
        }
    }
}
