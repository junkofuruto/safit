﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Safit.Core.Services.Auth;

public interface IBearerTokenGeneratorService
{
    public Task<string> GenerateAsync(long userId);
}