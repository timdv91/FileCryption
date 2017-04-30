using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace FileCryption
{
    public partial class Form1 : Form
    {
        byte[] keyFileData; //can only pass one var to background worker. So keyFileData is publicaly available.
        List<int[]> rebuildEncryptedDataList = new List<int[]>(); //list is more dynamic as array.

        //instantiate background workers publicaly:
        BackgroundWorker bw0 = new BackgroundWorker();
        BackgroundWorker bw1 = new BackgroundWorker();

        //use counters to keep track of finished work:
        static int BackgroundWorkersAmount = 2;
        int BackgroundWorkersCompleted = 0;

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

            bw1.DoWork += Bw1_DoWork; ;
            bw1.WorkerReportsProgress = true;
            bw1.ProgressChanged += Bw1_ProgressChanged;
            bw1.RunWorkerCompleted += Bw1_RunWorkerCompleted;
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

            //reset some public vars before (re)using them:
            //clear buildlist:
            rebuildEncryptedDataList.Clear();
            //clear backgroundWorkersCompleted:
            BackgroundWorkersCompleted = 0;

            //load the key file and file to encrypt:
            rwBinaryFile rwB = new rwBinaryFile();
            keyFileData = rwB.readBinaryFile(txtKeyfilePath.Text.ToString());
            byte[] sourceFileToEncrypt = rwB.readBinaryFile(txtEncryptFilePath.Text.ToString());

            //Split the load of sourceFileToEncrypt to different Backgroundworkers:
            //WARNING! When sourceFileToEncrypt is odd, do not lose a byte when devission is not a round number!
            float fltDiv = (float)sourceFileToEncrypt.Count() / (float)BackgroundWorkersAmount;
            int intDiv = sourceFileToEncrypt.Count() / BackgroundWorkersAmount;
            int restDiv = sourceFileToEncrypt.Count() % BackgroundWorkersAmount;

            Debug.WriteLine("fltDiv: " + fltDiv);
            Debug.WriteLine("intDiv: " + intDiv);
            Debug.WriteLine("restDiv: " + restDiv);

            byte[] bw0Data = new byte[intDiv];
            Array.Copy(sourceFileToEncrypt, 0, bw0Data, 0, intDiv);

            //last array has to carry the rest value:
            byte[] bw1Data = new byte[intDiv + restDiv];
            Array.Copy(sourceFileToEncrypt, intDiv, bw1Data, 0, intDiv + restDiv);

            //now encrypt the file:
            bw0.RunWorkerAsync(bw0Data); //keyfiledata is publicaly available.
            bw1.RunWorkerAsync(bw1Data);
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

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(0,_encryptedData);
        }

        //backgroundworker ID1:
        private void Bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] _sourceFileToEncrypt = (byte[])e.Argument; //retrieve needed data as argument.

            FileCrypt FC = new FileCrypt();
            int[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw1);

            //send data to RunWorkerCompleted:
            e.Result = encryptedData;
        }

        private void Bw1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //not used, Bw0 will fill progressbar.
        }

        private void Bw1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //get encrypted data from result:
            int[] _encryptedData = (int[])e.Result;

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(1, _encryptedData);
        }  

        //put all data inside 1 array and write it to file:
        void buildEncryptedArrayAndWriteToFile(int threadID ,int[] _encryptedData)
        {
            rebuildEncryptedDataList.Add(new int[_encryptedData.Count()]);
            rebuildEncryptedDataList.Insert(threadID, _encryptedData);

            //check if all workers are done before saving data:
            if (BackgroundWorkersCompleted < BackgroundWorkersAmount)
                return;

            //rebuild list with array (2D) to 1D array:
            List<int> dataCompleteList = new List<int>();
            foreach(int[] bwData in rebuildEncryptedDataList)
            {
                foreach(int encDataLine in bwData)
                {
                    if(encDataLine != 0) //skip '0' chars, no idea where comming from :(.
                        dataCompleteList.Add(encDataLine);
                }
            }

            //now write the data to file:
            rwBinaryFile rwB = new rwBinaryFile();
            rwB.writeEncryptedFile(saveFileToPath(), dataCompleteList.ToArray());
        }
    }
}
