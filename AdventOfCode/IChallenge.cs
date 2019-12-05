namespace AdventOfCode
{
    public interface IChallenge
    {
        public int Day { get; }
        public int Year { get; }
        public string Title { get; }

        public string[] Input { get; }

        public bool LoadInput();
        public void Solve();
    }
}
