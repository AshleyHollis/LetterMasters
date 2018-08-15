using System;
using System.Collections.Generic;

namespace LetterMasters.Common
{
    public class Response<T> : Response
    {
        public Response()
        {
            // This is a workaround for strings so I don't return nulls as the string class doesn't have a public paramterless constructor so it can't be newed up.
            if (typeof(T) == typeof(string))
            {
                Content = (T) (object) string.Empty;
            }
            else
            {
                Content = (T)Activator.CreateInstance(typeof(T));
            }
        }

        public T Content { get; set; }
    }

    public class Response
    {
        public Response()
        {
            Errors = new List<Exception>();
        }

        public bool IsSuccess { get; set; }
        public List<Exception> Errors { get; }

        public void AddErrors(Response response)
        {
            Errors.AddRange(response.Errors);
        }

        public void AddError(ApplicationException exception)
        {
            Errors.Add(exception);
        }
    }
}