using System;

namespace AISTek.DataFlow.Util
{
    public static class Tuple
    {
        public static Tuple<TX, TY> Create<TX, TY>(TX item1, TY item2)
        {
            return new Tuple<TX, TY>{Item1 = item1, Item2 = item2};
        }

        public static void Match<TX, TY>(this Tuple<TX, TY> tuple, Action<TX, TY> action)
        {
            action(tuple.Item1, tuple.Item2);
        }
    }
}
