namespace UnderTerminal
{
    internal class TunnelSimple
    {
        public TunnelSimple(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        //重写Tostring
        public override string ToString()
        {
            return Name;
        }
    }
}