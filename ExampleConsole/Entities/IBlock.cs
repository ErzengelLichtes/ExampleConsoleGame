namespace ExampleConsole.Entities
{
    interface IBlock : IPositioned
    {
        int Width { get; }
        int Height { get; }
    }
}
