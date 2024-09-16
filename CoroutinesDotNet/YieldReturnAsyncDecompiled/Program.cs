using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Security;
using System.Security.Permissions;
using System.Threading.Tasks.Sources;
using YieldReturnAsyncDecompiled;

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
    [Microsoft.CodeAnalysis.Embedded]
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
    [Microsoft.CodeAnalysis.Embedded]
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
    [Microsoft.CodeAnalysis.Embedded]
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
    private sealed class GetBotsWithNormalLoopAsync_d__2 : IAsyncEnumerable<Bot>, IAsyncEnumerator<Bot>, IAsyncDisposable, IValueTaskSource<bool>, IValueTaskSource, IAsyncStateMachine
    {
        public int state_1;

        public AsyncIteratorMethodBuilder t__builder;

        public ManualResetValueTaskSourceCore<bool> v_promiseOfValueOrEnd;

        private Bot current_2;

        private bool w_disposeMode;

        private int l_initialThreadId;

        private int count;

        public int count_3;

        private List<Bot> bots_5_1;

        private int i_5_2;

        private List<Bot>.Enumerator s_3;

        private Bot bot_5_4;

        private YieldAwaitable.YieldAwaiter u_1;

        Bot IAsyncEnumerator<Bot>.Current
        {
            [DebuggerHidden]
            get
            {
                return current_2;
            }
        }

        [DebuggerHidden]
        public GetBotsWithNormalLoopAsync_d__2(int state_1)
        {
            t__builder = AsyncIteratorMethodBuilder.Create();
            this.state_1 = state_1;
            l_initialThreadId = Environment.CurrentManagedThreadId;
        }

        private void MoveNext()
        {
            int num = state_1;
            try
            {
                YieldAwaitable.YieldAwaiter awaiter;
                switch (num)
                {
                    default:
                        if (!w_disposeMode)
                        {
                            num = (state_1 = -1);
                            bots_5_1 = new List<Bot>();
                            i_5_2 = 0;
                            goto IL_0110;
                        }
                        goto end_IL_0007;
                    case 0:
                        awaiter = u_1;
                        u_1 = default(YieldAwaitable.YieldAwaiter);
                        num = (state_1 = -1);
                        goto IL_00f5;
                    case -4:
                        break;
                    IL_00f5:
                        awaiter.GetResult();
                        i_5_2++;
                        goto IL_0110;
                    IL_0110:
                        if (i_5_2 < count)
                        {
                            List<Bot> list = bots_5_1;
                            Bot bot = new Bot();
                            bot.Id = i_5_2;
                            bot.Name = string.Format("Name {0}", i_5_2);
                            list.Add(bot);
                            awaiter = Task.Yield().GetAwaiter();
                            if (!awaiter.IsCompleted)
                            {
                                num = (state_1 = 0);
                                u_1 = awaiter;
                                GetBotsWithNormalLoopAsync_d__2 stateMachine = this;
                                t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                                return;
                            }
                            goto IL_00f5;
                        }
                        s_3 = bots_5_1.GetEnumerator();
                        break;
                }
                try
                {
                    if (num != -4)
                    {
                        goto IL_018d;
                    }
                    num = (state_1 = -1);
                    if (!w_disposeMode)
                    {
                        bot_5_4 = null;
                        goto IL_018d;
                    }
                    goto end_IL_013a;
                IL_018d:
                    if (s_3.MoveNext())
                    {
                        bot_5_4 = s_3.Current;
                        current_2 = bot_5_4;
                        num = (state_1 = -4);
                        goto IL_0221;
                    }
                end_IL_013a:;
                }
                finally
                {
                    if (num == -1)
                    {
                        ((IDisposable) s_3).Dispose();
                    }
                }
                if (!w_disposeMode)
                {
                    s_3 = default(List<Bot>.Enumerator);
                }
            end_IL_0007:;
            }
            catch (Exception exception)
            {
                state_1 = -2;
                current_2 = null;
                t__builder.Complete();
                v_promiseOfValueOrEnd.SetException(exception);
                return;
            }
            state_1 = -2;
            current_2 = null;
            t__builder.Complete();
            v_promiseOfValueOrEnd.SetResult(false);
            return;
        IL_0221:
            v_promiseOfValueOrEnd.SetResult(true);
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

        [DebuggerHidden]
        IAsyncEnumerator<Bot> IAsyncEnumerable<Bot>.GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken))
        {
            GetBotsWithNormalLoopAsync_d__2 GetBotsWithNormalLoopAsync_d;
            if (state_1 == -2 && l_initialThreadId == Environment.CurrentManagedThreadId)
            {
                state_1 = -3;
                t__builder = AsyncIteratorMethodBuilder.Create();
                w_disposeMode = false;
                GetBotsWithNormalLoopAsync_d = this;
            }
            else
            {
                GetBotsWithNormalLoopAsync_d = new GetBotsWithNormalLoopAsync_d__2(-3);
            }
            GetBotsWithNormalLoopAsync_d.count = count_3;
            return GetBotsWithNormalLoopAsync_d;
        }

        [DebuggerHidden]
        ValueTask<bool> IAsyncEnumerator<Bot>.MoveNextAsync()
        {
            if (state_1 == -2)
            {
                return default(ValueTask<bool>);
            }
            v_promiseOfValueOrEnd.Reset();
            GetBotsWithNormalLoopAsync_d__2 stateMachine = this;
            t__builder.MoveNext(ref stateMachine);
            short version = v_promiseOfValueOrEnd.Version;
            if (v_promiseOfValueOrEnd.GetStatus(version) == ValueTaskSourceStatus.Succeeded)
            {
                return new ValueTask<bool>(v_promiseOfValueOrEnd.GetResult(version));
            }
            return new ValueTask<bool>(this, version);
        }

        [DebuggerHidden]
        bool IValueTaskSource<bool>.GetResult(short token)
        {
            return v_promiseOfValueOrEnd.GetResult(token);
        }

        [DebuggerHidden]
        ValueTaskSourceStatus IValueTaskSource<bool>.GetStatus(short token)
        {
            return v_promiseOfValueOrEnd.GetStatus(token);
        }

        [DebuggerHidden]
        void IValueTaskSource<bool>.OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            v_promiseOfValueOrEnd.OnCompleted(continuation, state, token, flags);
        }

        [DebuggerHidden]
        void IValueTaskSource.GetResult(short token)
        {
            v_promiseOfValueOrEnd.GetResult(token);
        }

        [DebuggerHidden]
        ValueTaskSourceStatus IValueTaskSource.GetStatus(short token)
        {
            return v_promiseOfValueOrEnd.GetStatus(token);
        }

        [DebuggerHidden]
        void IValueTaskSource.OnCompleted(Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags)
        {
            v_promiseOfValueOrEnd.OnCompleted(continuation, state, token, flags);
        }

        [DebuggerHidden]
        ValueTask IAsyncDisposable.DisposeAsync()
        {
            if (state_1 >= -1)
            {
                throw new NotSupportedException();
            }
            if (state_1 == -2)
            {
                return default(ValueTask);
            }
            w_disposeMode = true;
            v_promiseOfValueOrEnd.Reset();
            GetBotsWithNormalLoopAsync_d__2 stateMachine = this;
            t__builder.MoveNext(ref stateMachine);
            return new ValueTask(this, v_promiseOfValueOrEnd.Version);
        }
    }

    [CompilerGenerated]
    private sealed class Md_0: IAsyncStateMachine
    {
        public int state_1;

        public AsyncTaskMethodBuilder t__builder;

        private TaskAwaiter u_1;

        private void MoveNext()
        {
            int num = state_1;
            try
            {
                TaskAwaiter awaiter;
                if (num != 0)
                {
                    awaiter = ProcessBotsAsync().GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        num = (state_1 = 0);
                        u_1 = awaiter;
                        Md_0 stateMachine = this;
                        t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                        return;
                    }
                }
                else
                {
                    awaiter = u_1;
                    u_1 = default(TaskAwaiter);
                    num = (state_1 = -1);
                }
                awaiter.GetResult();
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

    [CompilerGenerated]
    private sealed class ProcessBotsAsync_d__1: IAsyncStateMachine
    {
        public int state_1;

        public AsyncTaskMethodBuilder t__builder;

        private IAsyncEnumerator<Bot> s_1;

        private object s_2;

        private int s_3;

        private Bot bot_5_4;

        private bool s_5;

        private ValueTaskAwaiter<bool> u_1;

        private ValueTaskAwaiter u_2;

        private void MoveNext()
        {
            int num = state_1;
            try
            {
                ValueTaskAwaiter awaiter;
                if (num != 0)
                {
                    if (num == 1)
                    {
                        awaiter = u_2;
                        u_2 = default(ValueTaskAwaiter);
                        num = (state_1 = -1);
                        goto IL_01ab;
                    }
                    s_1 = GetBotsWithNormalLoopAsync(1000000).GetAsyncEnumerator(default(CancellationToken));
                    s_2 = null;
                    s_3 = 0;
                }
                object obj2;
                try
                {
                    if (num != 0)
                    {
                        goto IL_00b6;
                    }
                    ValueTaskAwaiter<bool> awaiter2 = u_1;
                    u_1 = default(ValueTaskAwaiter<bool>);
                    num = (state_1 = -1);
                goto IL_0118;
                IL_00b6:
                    awaiter2 = s_1.MoveNextAsync().GetAwaiter();
                    if (!awaiter2.IsCompleted)
                    {
                        num = (state_1 = 0);
                        u_1 = awaiter2;
                        ProcessBotsAsync_d__1 stateMachine = this;
                        t__builder.AwaitUnsafeOnCompleted(ref awaiter2, ref stateMachine);
                        return;
                    }
                    goto IL_0118;
                IL_0118:
                    s_5 = awaiter2.GetResult();
                    if (s_5)
                    {
                        bot_5_4 = s_1.Current;
                        if (bot_5_4.Id < 1000)
                        {
                            Console.WriteLine(string.Format("Id: {0}, Name: {1}", bot_5_4.Id, bot_5_4.Name));
                            bot_5_4 = null;
                            goto IL_00b6;
                        }
                    }
                }
                catch (Exception obj)
                {
                    obj2 = (s_2 = obj);
                }
                if (s_1 != null)
                {
                    awaiter = s_1.DisposeAsync().GetAwaiter();
                    if (!awaiter.IsCompleted)
                    {
                        num = (state_1 = 1);
                        u_2 = awaiter;
                        ProcessBotsAsync_d__1 stateMachine = this;
                        t__builder.AwaitUnsafeOnCompleted(ref awaiter, ref stateMachine);
                        return;
                    }
                    goto IL_01ab;
                }
                goto IL_01b3;
                IL_01ab:
                    awaiter.GetResult();
                    goto IL_01b3;
                IL_01b3:
                    obj2 = s_2;
                    if (obj2 != null)
                    {
                        Exception ex = obj2 as Exception;
                        if (ex == null)
                        {
                            throw (Exception)obj2;
                        }
                        ExceptionDispatchInfo.Capture(ex).Throw();
                    }
                    int s_6 = s_3;
                    s_2 = null;
                    s_1 = null;
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

    static async Task Main()
    {
        await M();
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

    [AsyncStateMachine(typeof(ProcessBotsAsync_d__1))]
    [DebuggerStepThrough]
    private static Task ProcessBotsAsync()
    {
        ProcessBotsAsync_d__1 stateMachine = new ProcessBotsAsync_d__1();
        stateMachine.t__builder = AsyncTaskMethodBuilder.Create();
        stateMachine.state_1 = -1;
        stateMachine.t__builder.Start(ref stateMachine);
        return stateMachine.t__builder.Task;
    }

    [AsyncIteratorStateMachine(typeof(GetBotsWithNormalLoopAsync_d__2))]
    private static IAsyncEnumerable<Bot> GetBotsWithNormalLoopAsync(int count)
    {
        GetBotsWithNormalLoopAsync_d__2 GetBotsWithNormalLoopAsync_d = new GetBotsWithNormalLoopAsync_d__2(-2);
        GetBotsWithNormalLoopAsync_d.count_3 = count;
        return GetBotsWithNormalLoopAsync_d;
    }
}