using System;
using System.Threading.Tasks;
namespace Beckamo.Services;

public interface IErrorHandler
{
    Task HandleAsync(Exception ex, string databaseName);
}