using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Core.Posts.Commands
{
    public class PostUpdateCommand : PostCommand
    {
        public int Id { get; set; }

    }
}
