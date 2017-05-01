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
        public UInt32[] encryptFile(byte[] keyFile, byte[] FileCryptFile, BackgroundWorker bw = null)
        {
            if (keyFile == null || FileCryptFile == null)
                return null; //error cant work with null vars.

            UInt32[] indexList = new UInt32[FileCryptFile.Count()]; //output file has same lenght as input file. Just scrambled bits. :)
            int indexListCounter = 0;

            foreach (byte b in FileCryptFile) //loop all bytes of data in file to fileCrypt.
            {
                //create random line picker for better encryption:
                int Seed = (int)DateTime.Now.Ticks;
                Random r0 = new Random(Seed);
                int ran0 = r0.Next(8196);

                ran0++;
                UInt32 startIndex = ((UInt32)keyFile.Count() / (UInt32)16); //start at 1/16th of the keyfile.

                //improve encryption by randomizing startPos:
                startIndex += (UInt32)ran0;

                //Debug.WriteLine("startindex: " + startIndex);

                for (UInt32 i =startIndex; i<keyFile.Count();i++) //loop all bytes of data in the keyFile.
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
                            Debug.WriteLine("ERROR keyfile not large enough!. Return null.");
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
        public byte[] decryptFile(byte[] keyFile, UInt32[] encryptedFileData)
        {
            if (keyFile == null || encryptedFileData == null)
                return null;

            byte[] decryptedFileData = new byte[encryptedFileData.Count()]; //create array with same length as encyrpted file array.

            UInt32 i = 0;
            foreach(UInt32 encryptIndex in encryptedFileData)
            {
                decryptedFileData[i] = keyFile[encryptIndex];
                i++;  
            }
            
            return decryptedFileData;
        }
    }
}
