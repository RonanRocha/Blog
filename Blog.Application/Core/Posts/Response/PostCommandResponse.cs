using Blog.Application.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Core.Posts.Response
{
    public class PostCommandResponse : IResponseBase
    {
        public bool IsValid { get; set; }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public IDictionary<string, string[]> Errors { get; set; }
    }
}
