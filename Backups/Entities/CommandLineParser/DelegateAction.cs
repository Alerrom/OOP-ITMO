using System.Collections.Generic;
using System.Linq;
using Backups.Abstractions;
using Backups.Entities.Commands;
using Backups.Tools;

namespace Backups.Entities.CommandLineParser
{
    public static class DelegateAction
    {
         public static IParseable ConvertToParseable(this ActionType actionType, IEnumerable<string> arguments)
                {
                    return actionType switch
                    {
                        ActionType.CreateBackupJob => new CreateBackupJob(arguments.Skip(1)),
                        ActionType.CreateRestorePoint => new CreateRestorePoint(arguments.Skip(1)),
                        ActionType.AddBackup => new AddBackup(arguments.Skip(1)),
                        ActionType.Info => new Info(arguments.Skip(1)),
                        _ => throw new UnknownArgumentException(arguments.ElementAt(0))
                    };
                }
    }
}