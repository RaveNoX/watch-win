#region Usings

using System;
using System.IO;
using System.Threading.Tasks;

#endregion

namespace WatchWin
{
    internal class Watcher : IDisposable
    {
        private readonly FileSystemWatcher _watcher;
        private bool _disposed;
        private readonly Func<object, Task<object>> _callback;

        public Watcher(string path, bool recursive, string fileFilter, WatchOptions watchOptions, Func<dynamic, Task<object>> callback)
        {
            if (string.IsNullOrWhiteSpace(path))
                throw new ArgumentNullException(nameof(path));

            if (callback == null)
                throw new ArgumentNullException(nameof(callback));

            if(watchOptions == null)
                throw new ArgumentNullException(nameof(watchOptions));

            _callback = callback;
            _watcher = new FileSystemWatcher(path)
            {
                InternalBufferSize = 4096 * 64,
                IncludeSubdirectories = recursive,
            };

            if (!string.IsNullOrWhiteSpace(fileFilter))
            {
                _watcher.Filter = fileFilter;
            }

            _watcher.NotifyFilter = watchOptions.ToNotifyFilters();
            _watcher.Changed += WatcherOnChanged;
            _watcher.Created += WatcherOnChanged;
            _watcher.Deleted += WatcherOnChanged;
            _watcher.Renamed += WatcherOnChanged;
            _watcher.Error += WatcherOnError;
        }

        public void Start()
        {
            if(_disposed) return;
            if(_watcher.EnableRaisingEvents) return;

            _watcher.EnableRaisingEvents = true;
        }

        private void WatcherOnError(object sender, ErrorEventArgs e)
        {
            _callback(new
            {
                type = "error",
                error = e.GetException().Message
            });
        }

        private void WatcherOnChanged(object sender, FileSystemEventArgs e)
        {
            _callback(new
            {
                type = e.ChangeType.ToString().ToLowerInvariant(),
                path = e.FullPath,
                name = e.Name
            });
        }

        public Guid Id { get; } = Guid.NewGuid();        

        public void Dispose()
        {
            if (_disposed) return;

            _watcher?.Dispose();

            _disposed = true;
        }
    }
}