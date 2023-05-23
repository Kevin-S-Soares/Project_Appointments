using Microsoft.AspNetCore.Mvc;

namespace Server.Services
{
    public class ServiceResponse<T> : ObjectResult
    {
        public ServiceResponse() : base(new()) { }

        public ServiceResponse(T data, int statusCode) : base(data)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public ServiceResponse(string? errorMessage, int statusCode) : base(errorMessage)
        {
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }



        public new object? Value
        {
            get
            {
                return Data is null ? ErrorMessage : Data;
            }
            set
            {
                _value = value;
            }
        }

        private object? _value;
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }

    }
}
