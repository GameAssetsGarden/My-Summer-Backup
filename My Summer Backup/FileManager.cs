using System.IO;

namespace My_Summer_Backup
{
    class FileManager
    {

        public static bool OVERWRITE_FILES = true;
        public static bool KEEP_FILES      = false;

        public static void CopyDirectory( string sourceDirectoryName, string destinationDirectoryName, bool copySubdirectories, bool overwriteFiles )
        {
            DirectoryInfo sourceDirectory = new DirectoryInfo( sourceDirectoryName );

            // Check if the source directory exists.
            if( !sourceDirectory.Exists )
            {
                return;
            }

            // Check if the destination directory exists. If not, create it.
            if( !Directory.Exists( destinationDirectoryName ) )
            {
                Directory.CreateDirectory( destinationDirectoryName );
            }

            // Get a list of all the files within the source directory.
            FileInfo[] sourceDirectoryFiles = sourceDirectory.GetFiles();

            // Copy all files to the destination directory.
            foreach( FileInfo file in sourceDirectoryFiles )
            {
                string destinationPath = Path.Combine( destinationDirectoryName, file.Name );
                file.CopyTo( destinationPath, overwriteFiles );
            }

            // Get a list of all subdirectories
            DirectoryInfo[] sourceSubdirectories = sourceDirectory.GetDirectories();

            // Copy source subdirectories to the destination directory if desired.
            if( copySubdirectories )
            {
                foreach ( DirectoryInfo subdirectory in sourceSubdirectories )
                {
                    string destinationPath = Path.Combine( destinationDirectoryName, subdirectory.Name );
                    CopyDirectory( subdirectory.FullName, destinationPath, copySubdirectories, OVERWRITE_FILES );
                }
            }
        }
    }
}
