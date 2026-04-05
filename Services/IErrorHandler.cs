<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
namespace Beckamo.Services;

public interface IErrorHandler
{
    Task HandleAsync(Exception ex, string databaseName);
=======
﻿using System;
using System.Threading.Tasks;
namespace Beckamo.Services;

public interface IErrorHandler
{
    Task HandleAsync(Exception ex, string databaseName);
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}