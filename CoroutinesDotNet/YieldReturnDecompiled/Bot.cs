using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace YieldReturnDecompiled
{
    public class Bot
    {
        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private int Id_K_BackingField;

        [CompilerGenerated]
        [DebuggerBrowsable(DebuggerBrowsableState.Never)]
        private string Name_K_BackingField;

        public int Id
        {
            [CompilerGenerated]
            get
            {
                return Id_K_BackingField;
            }
            [CompilerGenerated]
            set
            {
                Id_K_BackingField = value;
            }
        }

        public string Name
        {
            [CompilerGenerated]
            get
            {
                return Name_K_BackingField;
            }
            [CompilerGenerated]
            set
            {
                Name_K_BackingField = value;
            }
        }
    }
}
