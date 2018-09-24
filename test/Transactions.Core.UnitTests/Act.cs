using System;

namespace Transactions.Core.UnitTests
{
    public static class Act
    {
        public static void Safe(Action action)
        {
            try
            {
                action();
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}