using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace PDX.BLL.Helpers
{
    public class FileHelper
    {
        private static int DefaultBufferSize = 80 * 1024;

        /// <summary>
        /// Asynchronously saves the contents of an uploaded file.
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="file"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task SaveFileAsync(string directory, IFormFile file, CancellationToken cancellationToken = default(CancellationToken))
        {
            await SaveFileAsync(directory, file.FileName, file, cancellationToken);
        }

        public static async Task SaveFileAsync(string directory,string fileName, IFormFile file, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (file == null)
            {
                throw new ArgumentNullException(nameof(file));
            }
            if (!Directory.Exists(directory))
            {
                // Try to create the directory.
                Directory.CreateDirectory(directory);
            }
            var fileExtension = Path.GetExtension(file.FileName);

            using (var fileStream = new FileStream(Path.Combine(directory, $"{fileName}{fileExtension}"), FileMode.Create))
            {
                var inputStream = file.OpenReadStream();
                await inputStream.CopyToAsync(fileStream, DefaultBufferSize, cancellationToken);
            }
        }

        public static async Task SaveFileAsync(string directory,string fileName, byte[] bytes)
        {
            if (bytes == null)
            {
                throw new ArgumentNullException(nameof(bytes));
            }
            if (!Directory.Exists(directory))
            {
                // Try to create the directory.
                Directory.CreateDirectory(directory);
            }

            using (var sourceStream  = new FileStream(Path.Combine(directory, fileName), FileMode.Create))
            {
                sourceStream.Seek(0, SeekOrigin.End);
                await sourceStream.WriteAsync(bytes, 0, bytes.Length);
            }
        }

        /// <summary>
        /// Save multiple files
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="files"></param>
        /// <returns></returns>
        public static async Task SaveFileAsync(string directory, IFormFileCollection files)
        {
            foreach (var file in files)
            {
                await SaveFileAsync(directory, file);
            }
        }

        /// <summary>
        /// Read file from specified filepath
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static Byte[] ReadFile(string filePath)
        {
            Byte[] b = System.IO.File.ReadAllBytes(filePath);
            return b;
        }

        /// <summary>
        /// Read file from specified filepath as Base64 string
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFileAsBase64(string filePath){
            var bytes = ReadFile(filePath);
            var base64 = Convert.ToBase64String(bytes);
            return base64;
        }

        /// <summary>
        /// Read file from specified filepath as string
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFileAsString(string filePath){
            string fileString =  File.ReadAllText(filePath);
            return fileString;
        }
    }
}