using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileCryption
{
    class rwBinaryFile
    {
        //read a file in as binary array:
        public byte[] readBinaryFile(string filePath)
        {
            try
            {
                byte[] fileBytes = File.ReadAllBytes(filePath);
                return fileBytes;
            }catch(Exception)
            {
                return null;
            }
        }

        //write a binary array to a file:
        public bool writeBinaryFile(string strPath, byte[] dataToWrite)
        {
            if (dataToWrite == null || strPath == null)
                return false;

            File.WriteAllBytes(strPath, dataToWrite);
            return true;
        }

        //write encrypted file using streamwriter:
        public bool writeEncryptedFile(string strPath, UInt32[] dataToWrite)
        {
            if (strPath == null)
                return false;

            StreamWriter sw = new StreamWriter(strPath);

            foreach(UInt32 i in dataToWrite)
                sw.WriteLine(i);

            sw.Close();
            return true;
        }

        //read encrypted file using streamwriter:
        public UInt32[] readEncryptedFile(string strPath)
        {
            try
            {
                List<UInt32> indexList = new List<UInt32>();
                StreamReader sr = new StreamReader(strPath);

                //read line by line from file:
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    indexList.Add(Convert.ToUInt32(line)); //put data into list;
                }

                sr.Close();

                //convert list to array:
                UInt32[] indexArray = new UInt32[indexList.Count];
                for (int i = 0; i < indexList.Count; i++)
                    indexArray[i] = indexList[i];

                //return array:
                return indexArray;
            }catch(Exception)
            {
                return null;
            }
        }

        //returns the file extention
        public string getBinaryFileExtention(string filePath)
        {
            string[] strBuf = filePath.Split('.');          
            return strBuf[strBuf.Count()-1];
        }

        public string getEncryptedFileExtention(string filePath)
        {
            string[] strBuf = filePath.Split('.');
            return strBuf[strBuf.Count() - 2];
        }
    }
}
