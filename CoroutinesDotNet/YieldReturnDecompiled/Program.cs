using System.Collections;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Permissions;
using Microsoft.CodeAnalysis;
using YieldReturnDecompiled;

[assembly: CompilationRelaxations(8)]
[assembly: RuntimeCompatibility(WrapNonExceptionThrows = true)]
[assembly: Debuggable(DebuggableAttribute.DebuggingModes.Default | DebuggableAttribute.DebuggingModes.DisableOptimizations | DebuggableAttribute.DebuggingModes.IgnoreSymbolStoreSequencePoints | DebuggableAttribute.DebuggingModes.EnableEditAndContinue)]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
[assembly: AssemblyVersion("0.0.0.0")]
[module: UnverifiableCode]
namespace Microsoft.CodeAnalysis
{
    [CompilerGenerated]
    [Embedded]
    internal sealed class EmbeddedAttribute : Attribute
    {
    }
}
namespace System.Runtime.CompilerServices
{
    [CompilerGenerated]
    [Embedded]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
    internal sealed class NullableAttribute : Attribute
    {
        public readonly byte[] NullableFlags;

        public NullableAttribute(byte P_0)
        {
            byte[] array = new byte[1];
            array[0] = P_0;
            NullableFlags = array;
        }

        public NullableAttribute(byte[] P_0)
        {
            NullableFlags = P_0;
        }
    }
    [CompilerGenerated]
    [Embedded]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
    internal sealed class NullableContextAttribute : Attribute
    {
        public readonly byte Flag;

        public NullableContextAttribute(byte P_0)
        {
            Flag = P_0;
        }
    }
    [CompilerGenerated]
    [Embedded]
    [AttributeUsage(AttributeTargets.Module, AllowMultiple = false, Inherited = false)]
    internal sealed class RefSafetyRulesAttribute : Attribute
    {
        public readonly int Version;

        public RefSafetyRulesAttribute(int P_0)
        {
            Version = P_0;
        }
    }
}

public class C
{
    [CompilerGenerated]
    private sealed class GetBotsWithYield_d__2 : IEnumerable<Bot>, IEnumerable, IEnumerator<Bot>, IDisposable, IEnumerator
    {
        private int state_1;

        private Bot current_2;

        private int l_initialThreadId;

        private int count;

        public int count_3;

        private int i_5_1;

        Bot IEnumerator<Bot>.Current
        {
            [DebuggerHidden]
            get
            {
                return current_2;
            }
        }

        object IEnumerator.Current
        {
            [DebuggerHidden]
            get
            {
                return current_2;
            }
        }

        [DebuggerHidden]
        public GetBotsWithYield_d__2(int state_1)
        {
            this.state_1 = state_1;
            l_initialThreadId = Environment.CurrentManagedThreadId;
        }

        [DebuggerHidden]
        void IDisposable.Dispose()
        {
        }

        private bool MoveNext()
        {
            int num = state_1;
            if (num != 0)
            {
                if (num != 1)
                {
                    return false;
                }
                state_1 = -1;
                i_5_1++;
            }
            else
            {
                state_1 = -1;
                i_5_1 = 0;
            }
            if (i_5_1 < count)
            {
                Bot bot = new Bot();
                bot.Id = i_5_1;
                bot.Name = string.Format("Name {0}", i_5_1);
                current_2 = bot;
                state_1 = 1;
                return true;
            }
            return false;
        }

        bool IEnumerator.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            return this.MoveNext();
        }

        [DebuggerHidden]
        void IEnumerator.Reset()
        {
            throw new NotSupportedException();
        }

        [DebuggerHidden]
        IEnumerator<Bot> IEnumerable<Bot>.GetEnumerator()
        {
            GetBotsWithYield_d__2 GetBotsWithYield_d_;
            if (state_1 == -2 && l_initialThreadId == Environment.CurrentManagedThreadId)
            {
                state_1 = 0;
                GetBotsWithYield_d_ = this;
            }
            else
            {
                GetBotsWithYield_d_ = new GetBotsWithYield_d__2(0);
            }
            GetBotsWithYield_d_.count = count_3;
            return GetBotsWithYield_d_;
        }

        [DebuggerHidden]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<Bot>)this).GetEnumerator();
        }
    }

    [CompilerGenerated]
    private sealed class Md_0: IAsyncStateMachine
    {
        public int state_1;

        public AsyncTaskMethodBuilder t__builder;

        private void MoveNext()
        {
            int num = state_1;
            try
            {
                ProcessBots();
            }
            catch (Exception exception)
            {
                state_1 = -2;
                t__builder.SetException(exception);
                return;
            }
            state_1 = -2;
            t__builder.SetResult();
        }

        void IAsyncStateMachine.MoveNext()
        {
            //ILSpy generated this explicit interface implementation from .override directive in MoveNext
            this.MoveNext();
        }

        [DebuggerHidden]
        private void SetStateMachine(IAsyncStateMachine stateMachine)
        {
        }

        void IAsyncStateMachine.SetStateMachine(IAsyncStateMachine stateMachine)
        {
            //ILSpy generated this explicit interface implementation from .override directive in SetStateMachine
            this.SetStateMachine(stateMachine);
        }
    }

    [AsyncStateMachine(typeof(Md_0))]
    [DebuggerStepThrough]
    private static Task M()
    {
        Md_0 stateMachine = new Md_0();
        stateMachine.t__builder = AsyncTaskMethodBuilder.Create();
        stateMachine.state_1 = -1;
        stateMachine.t__builder.Start(ref stateMachine);
        return stateMachine.t__builder.Task;
    }

    static void Main() { M(); }

    private static void ProcessBots()
    {
        IEnumerable<Bot> botsWithYield = GetBotsWithYield(1000000);
        IEnumerator<Bot> enumerator = botsWithYield.GetEnumerator();
        try
        {
            while (enumerator.MoveNext())
            {
                Bot current = enumerator.Current;
                if (current.Id < 1000)
                {
                    Console.WriteLine(string.Format("Id: {0}, Name: {1}", current.Id, current.Name));
                    continue;
                }
                break;
            }
        }
        finally
        {
            if (enumerator != null)
            {
                enumerator.Dispose();
            }
        }
    }

    [IteratorStateMachine(typeof(GetBotsWithYield_d__2))]
    private static IEnumerable<Bot> GetBotsWithYield(int count)
    {
        GetBotsWithYield_d__2 GetBotsWithYield_d = new GetBotsWithYield_d__2(-2);
        GetBotsWithYield_d.count_3 = count;
        return GetBotsWithYield_d;
    }
}
