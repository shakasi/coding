using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.IO;
using System.ServiceModel.Web;
using System.Runtime.Serialization;

namespace FilesContract.Demo
{
    [ServiceContract]
    public interface IFilesService
    {
        //[WebGet(UriTemplate = "{serverfile}")]
        Stream DownloadFileStream(string serverfile);

        [WebGet(UriTemplate = "{*serverfile}?offset={offset}&count={count}")]
        [OperationContract(Name = "DownloadFileStreamExt")]
        Stream DownloadFileStream(string serverfile,  long offset = 0, long count = 0);

        [WebGet()]
        List<CusFileInfo> GetFiles();

        [WebGet(UriTemplate = "{*serverfile}")]
        string GetFile(string serverfile);

        string GetMD5HashFromFile(string serverfile);
    }

    [DataContract]
    public class CusFileInfo
    {
        public string filepath;

        [DataMember]
        public string FileName { get; set; }
        [DataMember]
        public long FileLength { get; set; }
        //[DataMember]
        //public string MapPath { get; set; }
    }
}