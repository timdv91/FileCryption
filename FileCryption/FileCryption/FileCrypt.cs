using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Diagnostics;

namespace FileCryption
{
    class FileCrypt
    {
        

        //Switches the file data with index number where same byte is found in keyfile: :)
        public int[] encryptFile(byte[] keyFile, byte[] FileCryptFile, BackgroundWorker bw = null)
        {
            if (keyFile == null || FileCryptFile == null)
                return null; //error cant work with null vars.

            int[] indexList = new int[FileCryptFile.Count()]; //output file has same lenght as input file. Just scrambled bits. :)
            int indexListCounter = 0;

            foreach (byte b in FileCryptFile) //loop all bytes of data in file to fileCrypt.
            {
                //create random line picker for better encryption:
                int Seed = (int)DateTime.Now.Ticks;
                Random r0 = new Random(Seed);
                int ran0 = r0.Next(1000);

                int startIndex = keyFile.Count() / ran0;

                Debug.WriteLine("random: " + ran0 + " / startpos: " + startIndex);

                for (int i=startIndex; i<keyFile.Count();i++) //loop all bytes of data in the keyFile.
                {
                    if (b == keyFile[i]) //if FileCryptData = KeyFileData then add this index to indexList.
                    {
                        indexList[indexListCounter] = i; //add index to indexlist, 
                        indexListCounter++; //increment indexListCounter;

                        break; //no need to continou this loop;
                    }
                    else
                    {
                        if(i == keyFile.Count()-1)
                        {
                            //error, no equivalent of data in the keyfile.
                            return null;
                        }
                    }
                }

                if (bw != null)
                {
                    float perc = ((float)indexListCounter / (float)FileCryptFile.Count()) * 100;
                    Console.Write("Perc: " + perc);
                    bw.ReportProgress(Convert.ToInt32(perc));
                }
            }
            return indexList;
        }

        //Decrypts file using 'encryptedFileData' index numbers to find correct byte data in keyfile:
        public byte[] decryptFile(byte[] keyFile, int[] encryptedFileData)
        {
            if (keyFile == null || encryptedFileData == null)
                return null;

            byte[] decryptedFileData = new byte[encryptedFileData.Count()]; //create array with same length as encyrpted file array.

            int i = 0;
            foreach(int encryptIndex in encryptedFileData)
            {
                decryptedFileData[i] = keyFile[encryptIndex];
                i++;  
            }
            
            return decryptedFileData;
        }
    }
}
