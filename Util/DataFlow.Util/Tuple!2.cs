namespace AISTek.DataFlow.Util
{
    public struct Tuple<TX, TY>
    {
        public TX Item1 { get; internal set; }
        public TY Item2 { get; internal set; }
    }
}
