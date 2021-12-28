namespace PS.Core.Abstract
{
    public interface IResult
    {
        string[] Errors { get; set; }

        bool IsSucceeded { get; }
    }
    public interface IResult<out T> : IResult
    {
        T Data { get; }
    }
}
