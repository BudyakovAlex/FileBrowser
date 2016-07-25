using System.Collections.Generic;

namespace FileBrowser.Models
{
    public class FilesInfo
    {
        public List<File> Files { get; set; }

        public string ParentDirectory { get; set; }

        public int CountSmallSize { get; set; }

        public int CountMediumSize { get; set; }

        public int CountBigSize { get; set; }
    }
}