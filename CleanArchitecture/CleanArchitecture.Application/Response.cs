namespace CleanArchitecture.Application
{
    public class Error
    {
        public int Code { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public string DebugMessage { get; set; }
    }

    public static class Response
    {
        public static Response<TData> Failure<TData>(Error error)
        {
            return new Response<TData>(false, default, error);
        }

        public static Response<TData> Ok<TData>(TData value = default)
        {
            return new Response<TData>(true, value, null);
        }
    }

    public class Response<TData>
    {
        public Response(bool succeed, TData value, Error error)
        {
            Value = value;
            Error = error;
            Succeed = succeed;
        }

        public TData Value { get; }
        public bool Succeed { get; }
        public Error Error { get; }
    }
}