#region Usings

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#endregion

#pragma warning disable 1998
// ReSharper disable ConsiderUsingAsyncSuffix

namespace WatchWin
{
    /// <summary>
    ///     Entry point
    /// </summary>
    public sealed class Startup
    {

        public async Task<object> Invoke(IDictionary<string, dynamic> args)
        {
            if (args == null)
                throw new ArgumentNullException(nameof(args));            

            var path = args.ContainsKey("path") ? args["path"] : null;
            var options = args.ContainsKey("options") ? args["options"] : null;
            var filter = args.ContainsKey("filter") ? (string)args["filter"] : null;
            var recursive = !args.ContainsKey("recursive") || (bool) args["recursive"];
            var callback = args.ContainsKey("callback") ? (Func<dynamic, Task<object>>) args["callback"] : null;

            var tcs = new TaskCompletionSource<object>();
            var watcher = new Watcher(path, recursive, filter, new WatchOptions(options), callback);

            return new
            {
                id = (Func<dynamic, Task<object>>)(async (fnArgs) => watcher.Id),
                start = (Func<dynamic, Task<object>>)((fnArgs) =>
                {
                    watcher.Start();
                    return tcs.Task;
                }),
                close = (Func<dynamic, Task<object>>)(async (fnArgs) =>
               {
                   watcher.Dispose();
                   tcs.TrySetResult(null);
                   return null;
               })
            };
        }

    }
}