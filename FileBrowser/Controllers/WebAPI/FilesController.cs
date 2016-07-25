using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using FileBrowser.Models;
using File = FileBrowser.Models.File;

namespace FileBrowser.Controllers.WebAPI
{
    public class FilesController : ApiController
    {
        public HttpResponseMessage Get(string path)
        {
            FilesInfo filesInfo;

            // Check directory path
            if (path == "..")
            {
                // Getting all local drives 
                DriveInfo[] drives = DriveInfo.GetDrives();

                filesInfo = new FilesInfo {ParentDirectory = "..", Files = new List<File>()};

                // Adding all drives name to list
                foreach (var drive in drives)
                {
                    filesInfo.Files.Add(new File() {Name = drive.RootDirectory.FullName});
                }
                return Request.CreateResponse(HttpStatusCode.OK, filesInfo);
            }

            filesInfo = new FilesInfo();

            var directory = new DirectoryInfo(path);

            var files = new List<File>();

            // Checking parent directory path
            filesInfo.ParentDirectory = directory.Parent?.FullName ?? "..";

            try
            {
                foreach (var directories in directory.GetDirectories())
                {
                    files.Add(new File() {Name = directories.FullName});
                }

                // Checking file length
                foreach (var file in directory.GetFiles())
                {
                    files.Add(new File() {Name = file.FullName});

                    if (file.Length < 10485760)
                    {
                        filesInfo.CountSmallSize++;
                    }
                    else
                    {
                        if (file.Length >= 10485760 && file.Length <= 50428800)
                        {
                            filesInfo.CountMediumSize++;
                        }
                        else
                        {
                            if (file.Length > 100057600)
                            {
                                filesInfo.CountBigSize++;
                            }
                        }
                    }
                }

                filesInfo.Files = files;

                return Request.CreateResponse(HttpStatusCode.OK, filesInfo);
            }
            catch
            {
                return Request.CreateResponse(InternalServerError());
            }
        }

    }
}
