namespace ChatSystem.Infrastructure.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    public class InternalResult<T>
    {
        private readonly HashSet<string> errors = new HashSet<string>();

        public InternalResult(T data, int code = (int)HttpStatusCode.OK)
        {
            Data = data;
            IsSuccess = true;
            Code = code;
        }

        public InternalResult(string message, int code, string error)
            : this(message, code)
        {
            if (string.IsNullOrWhiteSpace(error))
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Errors)}");
            }

            errors.Add(error);
        }

        public InternalResult(string message, int code, IEnumerable<string> errorList)
            : this(message, code)
        {
            if (!errors.Any())
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Errors)}");
            }

            errorList.Select(e => errors.Add(e));
        }

        private InternalResult(string message, int code)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                throw new ArgumentNullException($"{nameof(InternalResult<T>)}.{nameof(Message)}");
            }

            Message = message;
            Code = code;
        }

        public T Data { get; }

        public bool IsSuccess { get; }

        public int Code { get; }

        public string Message { get; }

        public IEnumerable<string> Errors { get { return new HashSet<string>(errors); } }
    }
}
