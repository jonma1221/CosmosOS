using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CosmosKernel1
{
    internal class File
    {
        String fileName = "";
        String fileID = "";
        String creationDate = "";
        int fileSize = 0; //Size is in bytes. Each char = 1 byte.
        List<String> fileData = new List<String>();
        String extension;

        public File(String fn)
        {
            fileName = fn;
            fileID = (Kernel.baseFileID).ToString();
            Kernel.baseFileID++;
            // creationDate = DateTime.Now.ToString("M/d/yyyy");
        }
        public void setExtension(string extension)
        {
            this.extension = extension;
        }
        public void setCreationDate(string CreationDate)
        {
            this.creationDate = CreationDate;
        }
        public void setFileSize(int fileSize)
        {
            this.fileSize = fileSize;
        }
        public void setFileData(List<string> fd)
        {
            fileData = fd;
        }

        public String getFileName()
        {
            return fileName;
        }
        public String getFileId()
        {
            return fileID;
        }
        public string getCreationDate()
        {
            return creationDate;
        }
        public string getFileSize()
        {
            updateFileSize();
            return fileSize.ToString();
        }
        public List<String> getFileData()
        {
            return fileData;
        }
        public String getExtension()
        {
            return extension;
        }

        public void updateFileSize()
        {
            int characters = 0;
            for (int j = 0; j < fileData.Count; j++)
            {

                characters = characters + fileData[j].Length;
            }
            fileSize = characters;
        }


    }
    //class File
    //{
    //    string fileId;
    //    String extension;
    //    string CreationDate;
    //    int fileSize;
    //    List<string> fileData;

    //    public File(string fileId)
    //    {
    //        this.fileId = fileId;
    //        //this.CreationDate = CreationDate;
    //        //this.fileSize = fileSize;
    //        //this.fileData = fileData;
    //    }
    //    public void setFileId(string fileId)
    //    {
    //        this.fileId = fileId;
    //    }
    //    public void setExtension(string extension)
    //    {
    //        this.extension = extension;
    //    }
    //    public void setCreationDate(string CreationDate)
    //    {
    //        this.CreationDate = CreationDate;
    //    }
    //    public void setFileSize(int fileSize)
    //    {
    //        this.fileSize = fileSize;
    //    }
    //    public void setFileData(List<string> fileData)
    //    {
    //        this.fileData = fileData;
    //    }
    //    public string getFileId()
    //    {
    //        return fileId;
    //    }
    //    public string getExtension()
    //    {
    //        return extension;
    //    }
    //    public string getCreationDate()
    //    {
    //        return CreationDate;
    //    }
    //    public int getFileSize()
    //    {
    //        return fileSize;
    //    }
    //    public List<string> getFileData()
    //    {
    //        return fileData;
    //    }
    //}
}
