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
        public bool writeEncryptedFile(string strPath, int[] dataToWrite)
        {
            if (strPath == null)
                return false;

            StreamWriter sw = new StreamWriter(strPath);

            foreach(int i in dataToWrite)
                sw.WriteLine(i);

            sw.Close();
            return true;
        }

        //read encrypted file using streamwriter:
        public int[] readEncryptedFile(string strPath)
        {
            try
            {
                List<int> indexList = new List<int>();
                StreamReader sr = new StreamReader(strPath);

                //read line by line from file:
                while (true)
                {
                    string line = sr.ReadLine();
                    if (line == null)
                    {
                        break;
                    }

                    indexList.Add(Convert.ToInt32(line)); //put data into list;
                }

                sr.Close();

                //convert list to array:
                int[] indexArray = new int[indexList.Count];
                for (int i = 0; i < indexList.Count; i++)
                    indexArray[i] = indexList[i];

                //return array:
                return indexArray;
            }catch(Exception)
            {
                return null;
            }
        }
    }
}
