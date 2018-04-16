using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;

namespace BigFileUploader
{
    [EnableCors("*", "*", "*")]
    public class FileUploadController : ApiController
    {
        [HttpGet]
        public async Task<object> Create(long fileSize, long blockSize = 1024 * 64)
        {
            var token = Path.GetRandomFileName();
            var meta = UploadMetaData.Create(fileSize, blockSize);

            JObject.FromObject(new
            {
                fileSize,
                blockSize,
                blocks = new bool[fileSize / blockSize + (fileSize % blockSize == 0 ? 0 : 1)],
            });

            var filepath = HostingEnvironment.MapPath("~/temp/" + token);
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));

            File.WriteAllBytes(filepath, new byte[0]);
            SaveMeta(token, meta);

            return new
            {
                //filepath,
                token,
            };
        }

        private static void SaveMeta(string token, UploadMetaData meta)
        {
            var filepath = HostingEnvironment.MapPath("~/tokens/" + token + ".json");
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));
            File.WriteAllText(filepath, meta.ToString());
        }

        private async Task<UploadMetaData> LoadMeta(string token)
        {
            var filepath = HostingEnvironment.MapPath("~/tokens/" + token + ".json");

            if (File.Exists(filepath) == false)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.NotFound, "invalid token"));

            using (var stream = File.OpenRead(filepath))
            {
                return UploadMetaData.FromJson(await new StreamReader(stream).ReadToEndAsync());
            }
        }

        [HttpPost]
        public async Task<object> Upload(string token, int blockIndex)
        {
            var meta = await LoadMeta(token);


            if (blockIndex < 0 || blockIndex >= meta.Blocks.Length)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "block index is out of range."));

            var expectSize = blockIndex < meta.Blocks.Length - 1 ? meta.BlockSize : meta.FileSize - blockIndex * meta.BlockSize;

            var content = await ReadContent();

            if (content.Length != expectSize)
                throw new HttpResponseException(Request.CreateErrorResponse(HttpStatusCode.BadRequest, "content size is invalid."));


            var filepath = HostingEnvironment.MapPath("~/temp/" + token);
            using (var stream = File.Open(filepath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
            {
                stream.Seek(blockIndex * meta.BlockSize, SeekOrigin.Begin);
                await stream.WriteAsync(content, 0, content.Length);
            }

            meta.Blocks[blockIndex] = true;
            SaveMeta(token, meta);

            return await Task(token);
        }

        private async Task<byte[]> ReadContent()
        {
            var content = Request.Content;
            if (content.IsMimeMultipartContent())
                return await (await content.ReadAsMultipartAsync()).Contents[0].ReadAsByteArrayAsync();

            else
                return await content.ReadAsByteArrayAsync();
        }

        [HttpGet]
        public async Task<object> Task(string token, string filename = null)
        {
            var meta = await LoadMeta(token);
            int? incompleteBlock = 0;

            foreach (bool item in meta.Blocks)
            {
                if (item == false)
                    break;
                else
                    incompleteBlock++;
            }

            if (incompleteBlock >= meta.Blocks.Length)
            {
                if (filename != null)
                    return await Complete(token, filename);

                else
                    return new
                    {
                        meta.FileSize,
                        meta.BlockSize,
                        TotalBlocks = meta.Blocks.Length,
                        meta.IncompleteBlocks,
                        meta.Progress,
                        Token = token,
                    };
            }

            if (filename != null && incompleteBlock == null)
                return await Complete(token, filename);

            var start = incompleteBlock * meta.BlockSize;
            var end = (incompleteBlock + 1) * meta.BlockSize;
            if (end > meta.FileSize)
                end = meta.FileSize;

            return new
            {
                meta.FileSize,
                meta.BlockSize,
                TotalBlocks = meta.Blocks.Length,
                meta.IncompleteBlocks,
                meta.Progress,
                Token = token,

                incomplete = incompleteBlock == null ? null : new
                {
                    blockIndex = incompleteBlock,
                    start,
                    end,
                }
            };
        }

        private async Task<object> Complete(string token, string filename)
        {
            var meta = await LoadMeta(token);
            if (meta.Blocks.All(item => item) == false)
                throw new InvalidOperationException();

            var sourcePath = HostingEnvironment.MapPath("~/temp/" + token);
            var virtualPath = "~/files/" + token + "/" + Path.GetFileName(filename);
            var filepath = HostingEnvironment.MapPath(virtualPath);
            Directory.CreateDirectory(Path.GetDirectoryName(filepath));

            File.Move(sourcePath, filepath);
            File.Delete(HostingEnvironment.MapPath("~/tokens/" + token + ".json"));

            return new
            {
                url = Url.Content(virtualPath),
            };
        }

        private class UploadMetaData
        {
            public long FileSize { get; private set; }

            public long BlockSize { get; private set; }

            public bool[] Blocks { get; private set; }

            private int? _completed;
            public int CompletedBlocks { get { return _completed ?? (int)(_completed = Blocks.Count(item => item)); } }
            public int IncompleteBlocks { get { return Blocks.Length - CompletedBlocks; } }
            public decimal Progress { get { return CompletedBlocks / (decimal)Blocks.Length; } }

            private UploadMetaData() { }

            public static UploadMetaData Create(long fileSize, long blockSize)
            {
                var instance = new UploadMetaData();

                instance.FileSize = fileSize;
                instance.BlockSize = blockSize;

                var maxBlocks = fileSize / blockSize + (fileSize % blockSize == 0 ? 0 : 1);
                instance.Blocks = new bool[maxBlocks];
                return instance;

            }

            public static UploadMetaData FromJson(string json)
            {
                var data = JObject.Parse(json);

                var instance = new UploadMetaData();
                instance.FileSize = (long)data["FileSize"];
                instance.BlockSize = (long)data["BlockSize"];

                instance.Blocks = ((JArray)data["Blocks"]).ToObject<bool[]>();

                return instance;
            }

            public override string ToString()
            {
                return JObject.FromObject(this).ToString();
            }
        }
    }
}