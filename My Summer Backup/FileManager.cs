using System.IO;
using System;
using System.Text.RegularExpressions;

namespace My_Summer_Backup{

    class FileManager{

        public static bool   OVERWRITE_FILES   = true;
        public static bool   KEEP_FILES        = false;
        public static bool   COPY_SUBDIRS      = true;
        public static bool   LEAVE_SUBDIRS     = false;
        public static bool   DELETE_CONTENTS   = true;
        public static string gameDataDirectory = @"%USERPROFILE%\AppData\LocalLow\Amistech\My Summer Car";
        public static string backupDirectory   = @"My Summer Backups\";
        public static string NO_BACKUPS_FOUND  = "No Backups Found";
        public static string SELECT_BACKUP     = "Select Backup Folder";
        public static string SELECT_BACKUP_MSG = "Select a backup folder first.";
        public static string BACKUP_LOADED_MSG = " has been loaded."; // Append backup folder name to the beginning of this string.

        // Used to expand environment variables within a path. EX: %USERPROFILE%
        public static string ExpandPath( string initialPath ){

            string fullPath = Environment.ExpandEnvironmentVariables( initialPath );
            return fullPath;

        }

        public static string[] GetBackupFolderList(){
            
            DirectoryInfo backupDirectoryInfo = new DirectoryInfo( backupDirectory );

            if( backupDirectoryInfo.Exists ) {

                DirectoryInfo[] folderNamesInfo = backupDirectoryInfo.GetDirectories();

                int numFolders      = folderNamesInfo.GetLength( 0 ) + 1; // Add 1 to make room for the default selector.
                string[] folderList = new string[numFolders];

                folderList[0] = SELECT_BACKUP; // Set first index to the default selector.

                // Add all dynamic folders.
                for( int i = 1; i < numFolders; i++ ) {
                    folderList[i] = folderNamesInfo[i-1].Name; // Subract 1 to get proper folderNamesInfo[] index.
                }

                return folderList;

            }else{

                string[] folderList = new string[1];
                folderList[0]       = NO_BACKUPS_FOUND;

                return folderList;

            }

            
        }

        public static void CopyDirectory( string sourceDirectoryName, string destinationDirectoryName, bool copySubdirectories, bool overwriteFiles ){

            sourceDirectoryName      = ExpandPath( sourceDirectoryName  );
            destinationDirectoryName = ExpandPath( destinationDirectoryName );

            DirectoryInfo sourceDirectoryInfo      = new DirectoryInfo( sourceDirectoryName );
            DirectoryInfo destinationDirectoryInfo = new DirectoryInfo( destinationDirectoryName );

            // Check if the source directory exists.
            if( !sourceDirectoryInfo.Exists ){

                return;

            }

            // Check if the destination directory exists. If not, create it.
            if( !Directory.Exists( destinationDirectoryName ) ){

                Directory.CreateDirectory( destinationDirectoryName );

            }else if( Directory.Exists( destinationDirectoryName ) && overwriteFiles ) {

                // Delete subfolders and files.
                foreach( FileInfo file in destinationDirectoryInfo.GetFiles() ) {
                    file.Delete();
                }
                foreach( DirectoryInfo directory in destinationDirectoryInfo.GetDirectories() ) {
                    directory.Delete( DELETE_CONTENTS );
                }

                Directory.CreateDirectory( destinationDirectoryName );

            }

            // Get a list of all the files within the source directory.
            FileInfo[] sourceDirectoryFiles = sourceDirectoryInfo.GetFiles();

            // Copy all files to the destination directory.
            foreach( FileInfo file in sourceDirectoryFiles ){

                string destinationPath = Path.Combine( destinationDirectoryName, file.Name );
                file.CopyTo( destinationPath, overwriteFiles );

            }

            // Get a list of all subdirectories
            DirectoryInfo[] sourceSubdirectories = sourceDirectoryInfo.GetDirectories();

            // Copy source subdirectories to the destination directory if desired.
            if( copySubdirectories ){

                foreach ( DirectoryInfo subdirectory in sourceSubdirectories ){

                    string destinationPath = Path.Combine( destinationDirectoryName, subdirectory.Name );
                    CopyDirectory( subdirectory.FullName, destinationPath, copySubdirectories, OVERWRITE_FILES );

                }

            }

        }

    }

}
