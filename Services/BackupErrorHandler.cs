
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

}
