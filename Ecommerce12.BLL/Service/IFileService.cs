using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce12.BLL.Service
{
    public interface IFileService
    {
        public Task<string?> UploadeFile(IFormFile file);

    }
}
