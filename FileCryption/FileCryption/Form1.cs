using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileCryption
{
    public partial class Form1 : Form
    {
        byte[] keyFileData; //can only pass one var to background worker. So keyFileData is publicaly available.

        //instantiate background workers publicaly:
        BackgroundWorker bw0 = new BackgroundWorker();

        public Form1()
        {
            InitializeComponent();
            initBackgroundWorkers();
        }

        //initialize background workers:
        void initBackgroundWorkers()
        {
            bw0.DoWork += Bw0_DoWork;
            bw0.WorkerReportsProgress = true;
            bw0.ProgressChanged += Bw0_ProgressChanged;
            bw0.RunWorkerCompleted += Bw0_RunWorkerCompleted;
        }       

        //choose files buttons:
        private void btnKeyFile_Click(object sender, EventArgs e)
        {
            txtKeyfilePath.Text = getFilePath();
        }

        private void btnFileToEncrypt_Click(object sender, EventArgs e)
        {
            txtEncryptFilePath.Text = getFilePath();
        }

        private void btnFileToDecrypt_Click(object sender, EventArgs e)
        {
            txtDecryptFilePath.Text = getFilePath();
        }

        //run program buttons:
        private void btnRunEncryption_Click(object sender, EventArgs e)
        {
            //break when paths are missing:
            if (txtKeyfilePath.Text == null && txtEncryptFilePath.Text == null)
                return;
            //continue when paths are given.

            //load the key file and file to encrypt:
            rwBinaryFile rwB = new rwBinaryFile();
            keyFileData = rwB.readBinaryFile(txtKeyfilePath.Text.ToString());
            byte[] sourceFileToEncrypt = rwB.readBinaryFile(txtEncryptFilePath.Text.ToString());

            //now encrypt the file:
            bw0.RunWorkerAsync(sourceFileToEncrypt); //keyfiledata is publicaly available.
        }

        private void btnRunDecrypt_Click(object sender, EventArgs e)
        {
            //break when paths are missing:
            if (txtKeyfilePath.Text == null && txtDecryptFilePath.Text == null)
                return;
            //continue when paths are given.

            //load the key file and file to decrypt:
            rwBinaryFile rwB = new rwBinaryFile();
            byte[] keyFileData = rwB.readBinaryFile(txtKeyfilePath.Text.ToString());
            int[] sourceFileToDecrypt = rwB.readEncryptedFile(txtDecryptFilePath.Text.ToString());

            //now decrypt the file:
            FileCrypt FC = new FileCrypt();
            byte[] originalData = FC.decryptFile(keyFileData, sourceFileToDecrypt);

            //now write the data like an original file:
            rwB.writeBinaryFile(saveFileToPath(), originalData);        
        }

        //openfiledialog function:
        private string getFilePath()
        {
            OpenFileDialog OF = new OpenFileDialog();
            if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return OF.FileName.ToString();
            else
                return null;
        }

        //savefiledialog function
        private string saveFileToPath()
        {
            SaveFileDialog SF = new SaveFileDialog();
            if (SF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return SF.FileName.ToString();
            else
                return null;
        }

        //backgroundworker ID 0:
        private void Bw0_DoWork(object sender, DoWorkEventArgs e) 
        {
            byte[] _sourceFileToEncrypt = (byte[]) e.Argument; //retrieve needed data as argument.

            FileCrypt FC = new FileCrypt();
            int[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw0);

            //send data to RunWorkerCompleted:
            e.Result = encryptedData;
        }

        private void Bw0_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
        }

        private void Bw0_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //get encrypted data from result:
            int[] _encryptedData = (int[]) e.Result;

            //now write the data to file:
            rwBinaryFile rwB = new rwBinaryFile();
            rwB.writeEncryptedFile(saveFileToPath(), _encryptedData);
        }
    }
}
