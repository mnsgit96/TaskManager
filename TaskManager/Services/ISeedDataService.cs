﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Services
{
    public interface ISeedDataService
    {
        Task EnsureSeedData();
    }
}
