using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace My_Summer_Backup{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window{

        public MainWindow(){

            InitializeComponent();
            this.Title += " - v2017.4.5.1(Beta)"; // Append program version number.
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
    }

}