#region Usings

using System.IO;

#endregion

namespace WatchWin
{
    internal class WatchOptions
    {
        #region .ctor

        public WatchOptions()
        {
            FileName = true;
            DirectoryName = true;
            LastWrite = true;
        }

        public WatchOptions(dynamic args) : this()
        {
            if (args == null)
                return;

            FileName = args.fileName == true;
            DirectoryName = args.dirName == true;
            Attributes = args.attributes == true;
            Size = args.size == true;
            LastWrite = args.lastWrite == true;
            LastAccess = args.lastAccess == true;
            CreationTime = args.creationTime == true;
            Security = args.security == true;
        }
        #endregion

        #region Properties

        /// <summary>
        ///     The name of the file.
        /// </summary>
        public bool FileName { get; set; } = true;

        /// <summary>
        ///     The name of the directory.
        /// </summary>
        public bool DirectoryName { get; set; } = true;

        /// <summary>
        ///     The attributes of the file or folder.
        /// </summary>
        public bool Attributes { get; set; }

        /// <summary>
        ///     The size of the file or folder.
        /// </summary>
        public bool Size { get; set; }

        /// <summary>
        ///     The date the file or folder last had anything written to it.
        /// </summary>
        public bool LastWrite { get; set; } = true;

        /// <summary>
        ///     The date the file or folder was last opened.
        /// </summary>
        public bool LastAccess { get; set; }

        /// <summary>
        ///     The time the file or folder was created.
        /// </summary>
        public bool CreationTime { get; set; }

        /// <summary>
        ///     The security settings of the file or folder.
        /// </summary>
        public bool Security { get; set; }

        #endregion

        public NotifyFilters ToNotifyFilters()
        {
            NotifyFilters ret = 0;

            if (FileName)
                ret |= NotifyFilters.FileName;

            if (DirectoryName)
                ret |= NotifyFilters.DirectoryName;

            if (Attributes)
                ret |= NotifyFilters.Attributes;

            if (Size)
                ret |= NotifyFilters.Size;

            if (LastWrite)
                ret |= NotifyFilters.LastWrite;

            if (LastAccess)
                ret |= NotifyFilters.LastAccess;

            if (CreationTime)
                ret |= NotifyFilters.CreationTime;

            if (Security)
                ret |= NotifyFilters.Security;

            return ret;
        }
    }
}