using System.Text.RegularExpressions;
using System.Windows;

namespace My_Summer_Backup {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{

        public MainWindow(){

            InitializeComponent();
            this.Title = "My Summer Backup - v2017.4.14.1"; // Append program version number.
            FileManager.CreateDirectory( FileManager.backupDirectory );
            updateBackupList(); // Update the list of backup folders on program load.
        }

        private void createBackup( object sender, RoutedEventArgs e ){

            string backupFolderName = fileNameTextBox.Text;

            FileManager.CopyDirectory( FileManager.gameDataDirectory, FileManager.backupDirectory + backupFolderName, FileManager.LEAVE_SUBDIRS, FileManager.OVERWRITE_FILES );
            updateBackupList();

        }

        private void loadBackup( object sender, RoutedEventArgs e ) {

            // Determine if a list item is selected.
            if( fileNameListBox.SelectedIndex != -1 ){

                if( Regex.IsMatch( (string)fileNameListBox.SelectedValue, FileManager.NO_BACKUPS_FOUND ) ){

                    backupStatusTextBox.Text = FileManager.SELECT_BACKUP_MSG;

                }else if( Regex.IsMatch( (string)fileNameListBox.SelectedValue, FileManager.SELECT_BACKUP ) ){

                    backupStatusTextBox.Text = FileManager.SELECT_BACKUP_MSG;

                }else{

                    FileManager.CopyDirectory( FileManager.backupDirectory + fileNameListBox.SelectedValue, FileManager.gameDataDirectory, FileManager.LEAVE_SUBDIRS, FileManager.OVERWRITE_FILES );
                    backupStatusTextBox.Text = fileNameListBox.SelectedValue + FileManager.BACKUP_LOADED_MSG;
                }

            }else{

                backupStatusTextBox.Text = FileManager.SELECT_BACKUP_MSG;

            }
        }

        private void updateBackupList() {

            string[] backupFolderList = FileManager.GetBackupFolderList();
            int numBackups = backupFolderList.Length;

            fileNameListBox.Items.Clear(); // Clear backup list.

            for( int i = 0; i < numBackups; i++ ) {
                fileNameListBox.Items.Add( backupFolderList[i] );
            }
        }

        private void openMySummerCarFolder( object sender, RoutedEventArgs e ) {

            FileManager.OpenDirectory( FileManager.gameDataDirectory );

        }

        private void openBackupFolder( object sender, RoutedEventArgs e ) {

            FileManager.OpenDirectory( FileManager.backupDirectory );

        }

        private void openSourceCode( object sender, RoutedEventArgs e ) {

            FileManager.OpenWebsite( FileManager.GIT_SOURCE_REPO );

        }

        private void openProgramReleases( object sender, RoutedEventArgs e ) {

            FileManager.OpenWebsite( FileManager.GIT_PROG_RELEASES );

        }

        private void buyMySummerCar( object sender, RoutedEventArgs e ) {

            FileManager.OpenWebsite( FileManager.MSC_STEAM );

        }

        private void openMySummerCarCommunity( object sender, RoutedEventArgs e ) {

            FileManager.OpenWebsite( FileManager.MSC_COMMUNITY );

        }

        private void openMySummerCarWebsite( object sender, RoutedEventArgs e ) {

            FileManager.OpenWebsite( FileManager.MSC_WEBSITE );

        }
    }

}