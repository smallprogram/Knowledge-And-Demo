using System;
using System.Collections.Generic;
using System.Text;

namespace RESTFul.Services.Interface
{
    public interface IPropertyCheckerService
    {
        bool TypeHasProperites<T>(string fields);
    }
}
