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
        List<UInt32[]> rebuildEncryptedDataList = new List<UInt32[]>(); //list is more dynamic as array.

        //instantiate background workers publicaly:
        BackgroundWorker bw0 = new BackgroundWorker();
        BackgroundWorker bw1 = new BackgroundWorker();
        BackgroundWorker bw2 = new BackgroundWorker();
        BackgroundWorker bw3 = new BackgroundWorker();

        //use counters to keep track of finished work:
/*+1 if using all 4 bw's */        static int BackgroundWorkersAmount = 3;
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

            bw1.DoWork += Bw1_DoWork;
            bw1.WorkerReportsProgress = true;
            bw1.ProgressChanged += Bw1_ProgressChanged;
            bw1.RunWorkerCompleted += Bw1_RunWorkerCompleted;

            bw2.DoWork += Bw2_DoWork;
            bw2.WorkerReportsProgress = true;
            bw2.ProgressChanged += Bw2_ProgressChanged;
            bw2.RunWorkerCompleted += Bw2_RunWorkerCompleted;

            bw3.DoWork += Bw3_DoWork;
            bw3.WorkerReportsProgress = true;
            bw3.ProgressChanged += Bw3_ProgressChanged;
            bw3.RunWorkerCompleted += Bw3_RunWorkerCompleted;
        }

        //choose files buttons:
        private void btnKeyFile_Click(object sender, EventArgs e)
        {
            txtKeyfilePath.Text = getFilePath();

            if (txtKeyfilePath.Text != "")
            {
                btnKeyFile.Enabled = false;
                txtKeyfilePath.Enabled = false;
            }
        }

        private void btnFileToEncrypt_Click(object sender, EventArgs e)
        {
            txtEncryptFilePath.Text = getFilePath();
        }

        private void btnFileToDecrypt_Click(object sender, EventArgs e)
        {
            txtDecryptFilePath.Text = getFilePath(true); //true shows only fenc files
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

            int sourceAStartPos = intDiv * 0;
            byte[] bw0Data = new byte[intDiv];
            Array.Copy(sourceFileToEncrypt, sourceAStartPos, bw0Data, 0, intDiv);
            rebuildEncryptedDataList.Add(new UInt32[intDiv]);

            sourceAStartPos = intDiv * 1;
            byte[] bw1Data = new byte[intDiv];
            Array.Copy(sourceFileToEncrypt, sourceAStartPos, bw1Data, 0, intDiv);
            rebuildEncryptedDataList.Add(new UInt32[intDiv]);

            sourceAStartPos = intDiv * 2;
            byte[] bw2Data = new byte[intDiv];
            Array.Copy(sourceFileToEncrypt, sourceAStartPos, bw2Data, 0, intDiv);
            rebuildEncryptedDataList.Add(new UInt32[intDiv]);

            //last array has to carry the rest value:
//disabled -->            //sourceAStartPos = intDiv * 3;
            byte[] bw3Data = new byte[intDiv + restDiv];
            Array.Copy(sourceFileToEncrypt, sourceAStartPos, bw3Data, 0, intDiv + restDiv);
            rebuildEncryptedDataList.Add(new UInt32[intDiv + restDiv]);

            //now encrypt the file:
            bw0.RunWorkerAsync(bw0Data); //keyfiledata is publicaly available.
            bw1.RunWorkerAsync(bw1Data);
 //disabled -->           //bw2.RunWorkerAsync(bw2Data);
            bw3.RunWorkerAsync(bw3Data);
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
            UInt32[] sourceFileToDecrypt = rwB.readEncryptedFile(txtDecryptFilePath.Text.ToString());

            //now decrypt the file:
            FileCrypt FC = new FileCrypt();
            byte[] originalData = FC.decryptFile(keyFileData, sourceFileToDecrypt);

            //create filepath with correct extention:
            string FileExtention = rwB.getEncryptedFileExtention(txtDecryptFilePath.Text);
            string saveFilepath = saveFileToPath("Data Files (*." + FileExtention + ")|*."+ FileExtention, FileExtention);

            //now write the data like an original file:
            rwB.writeBinaryFile(saveFilepath, originalData);        
        }

        //openfiledialog function:
        private string getFilePath(bool filterFencFiles = false)
        {
            OpenFileDialog OF = new OpenFileDialog();

            if (filterFencFiles == true)  //show only the fenc files.
            {
                OF.Filter = "Data Files (*.fenc)|*.fenc";
                OF.DefaultExt = "fenc";
                OF.AddExtension = true;
            }

            if (OF.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                return OF.FileName.ToString();
            else
                return null;
        }

        //savefiledialog function
        private string saveFileToPath(string fileType = "Data Files (*.fenc)|*.fenc", string defaultExt = "fenc", bool addExtention = true)
        {
            SaveFileDialog SF = new SaveFileDialog();
            SF.Filter = fileType;
            SF.DefaultExt = defaultExt;
            SF.AddExtension = addExtention;

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
            UInt32[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw0);

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
            UInt32[] _encryptedData = (UInt32[]) e.Result;

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(0,_encryptedData);
        }

        //backgroundworker ID1:
        private void Bw1_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] _sourceFileToEncrypt = (byte[])e.Argument; //retrieve needed data as argument.

            FileCrypt FC = new FileCrypt();
            UInt32[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw1);

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
            UInt32[] _encryptedData = (UInt32[])e.Result;

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(1, _encryptedData);
        }

        //backgroundworker ID2:
        private void Bw2_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] _sourceFileToEncrypt = (byte[])e.Argument; //retrieve needed data as argument.

            FileCrypt FC = new FileCrypt();
            UInt32[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw2);

            //send data to RunWorkerCompleted:
            e.Result = encryptedData;
        }

        private void Bw2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //not used, Bw0 will fill progressbar.
        }

        private void Bw2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //get encrypted data from result:
            UInt32[] _encryptedData = (UInt32[])e.Result;

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(2, _encryptedData);
        }

        //backgroundworker ID3:
        private void Bw3_DoWork(object sender, DoWorkEventArgs e)
        {
            byte[] _sourceFileToEncrypt = (byte[])e.Argument; //retrieve needed data as argument.

            FileCrypt FC = new FileCrypt();
            UInt32[] encryptedData = FC.encryptFile(keyFileData, _sourceFileToEncrypt, bw3);

            //send data to RunWorkerCompleted:
            e.Result = encryptedData;
        }

        private void Bw3_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            //not used, Bw0 will fill progressbar.
        }

        private void Bw3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //get encrypted data from result:
            UInt32[] _encryptedData = (UInt32[])e.Result;

            BackgroundWorkersCompleted++; //add 1 to completed to keep track of progress.
            buildEncryptedArrayAndWriteToFile(3, _encryptedData);
        }
  

        //put all data inside 1 array and write it to file:
        void buildEncryptedArrayAndWriteToFile(int threadID , UInt32[] _encryptedData)
        {
            rebuildEncryptedDataList.Insert(threadID, _encryptedData);

            //check if all workers are done before saving data:
            if (BackgroundWorkersCompleted < BackgroundWorkersAmount)
                return;

            if (progressBar1.Value < 100)
            {
                MessageBox.Show("Error encrypting file.");
                return;
            }

            //rebuild list with array (2D) to 1D array:
            List<UInt32> dataCompleteList = new List<UInt32>();
            foreach(UInt32[] bwData in rebuildEncryptedDataList)
            {
                foreach(UInt32 encDataLine in bwData)
                {
                    if(encDataLine != 0) //skip '0' chars, no idea where comming from :(.
                        dataCompleteList.Add(encDataLine);
                }
            }

            //now write the data to file:
            rwBinaryFile rwB = new rwBinaryFile();


            //create save file containing filetype:
            string savePath = saveFileToPath();
            string saveFileExtention = rwB.getBinaryFileExtention(txtEncryptFilePath.Text);

            //add extetion to filename as dualextention:
            string[] savePathBuf = savePath.Split('.');
            savePath = ""; //clear savePath;
            for (int I=0;I < savePathBuf.Count();I++)
            {
                if (I == savePathBuf.Count() - 1) //add file extention;
                    savePath += '.' + saveFileExtention + '.'; //this could probably give errors when filepath already contains multiple '.'

                savePath += savePathBuf[I];
            }

            Debug.Print(savePath);

            rwB.writeEncryptedFile(savePath, dataCompleteList.ToArray());
        }
    }
}
