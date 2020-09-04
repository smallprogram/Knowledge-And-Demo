using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlazorServer.Pages
{
    public class Counter1base : ComponentBase
    {
        protected int currentCount1 = 0;

        protected void IncrementCount1()
        {
            currentCount1++;
        }
    }
}
