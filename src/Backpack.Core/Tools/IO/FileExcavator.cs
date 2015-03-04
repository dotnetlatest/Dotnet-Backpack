// /*
//  * -----------------------------------------------------------------
//  * © 2006-2015  MineCloud, Inc (http://www.minecloud.com)
//  * -----------------------------------------------------------------
//  */ 

using System.IO;

namespace Backpack.Core.Tools.IO
{
    public class FileExcavator
    {
        /// <summary>
        /// Traverses all subdirectories of the specified path to find file(s) with the specified extension.
        /// </summary>
        /// <param name="targetDirectory">The path of the directory to start the search in.</param>
        /// <param name="targetExtension">The target file extension.</param>
        /// <returns>The path of the files found with matching extensions.</returns>
        public static string[] GetFileWithExtension(string targetDirectory, string targetExtension)
        {
            return Directory.GetFiles(targetDirectory, string.Format("*.{0}", targetExtension), SearchOption.AllDirectories);
        }

        /// <summary>
        /// Deletes the directory in the file system.
        /// </summary>
        /// <param name="targetDirectory">The path of the directory to delete.</param>
        public static void DeleteDirectory(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                foreach (string file in Directory.GetFiles(targetDirectory))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                foreach (string dir in Directory.GetDirectories(targetDirectory))
                {
                    DeleteDirectory(dir);
                }

                Directory.Delete(targetDirectory, false);
            }
        }

        /// <summary>
        /// Clears all files and subdirectories under the directory.
        /// </summary>
        /// <param name="targetDirectory">The path of the directory to clear.</param>
        public static void ClearDirectory(string targetDirectory)
        {
            if (Directory.Exists(targetDirectory))
            {
                //Clear all files
                foreach (string file in Directory.GetFiles(targetDirectory))
                {
                    File.SetAttributes(file, FileAttributes.Normal);
                    File.Delete(file);
                }

                //Delete all subdirectories and file in said directories
                foreach (string dir in Directory.GetDirectories(targetDirectory))
                {
                    DeleteDirectory(dir);
                }
            }
        }
    }
}