<<<<<<< HEAD
﻿using System;
using System.Threading.Tasks;
using Serilog;
namespace Beckamo.Services;

public class BackupErrorHandler : IErrorHandler
{
    private readonly ILogger _logger;

    public BackupErrorHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Exception ex, string databaseName)
    {
        _logger.Error(ex, "Ошибка бекапа базы {Database}", databaseName);

       //в будущем улучшить логику обработки ошибок
    }
=======
﻿using System;
using System.Threading.Tasks;
using Serilog;
namespace Beckamo.Services;

public class BackupErrorHandler : IErrorHandler
{
    private readonly ILogger _logger;

    public BackupErrorHandler(ILogger logger)
    {
        _logger = logger;
    }

    public async Task HandleAsync(Exception ex, string databaseName)
    {
        _logger.Error(ex, "Ошибка бекапа базы {Database}", databaseName);

       //в будущем улучшить логику обработки ошибок
    }
>>>>>>> 4cd8a9c83d127e7b68b0c141b233ba7e0124c7bb
}