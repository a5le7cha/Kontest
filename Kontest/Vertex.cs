namespace Kontest
{
    public class Vertex
    {
        public int Number { get; set; }
        public int Marker { get; set; } = 0;
        public Vertex Parent { get; set; }
    }
}