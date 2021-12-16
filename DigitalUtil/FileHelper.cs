using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Linq;


namespace DigitalUtil
{
    public class FileHelper
    {
        ////TODO add log4net
        ////private static readonly log4net.ILog logger = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //public bool CheckMatchingName(string mimshakType, string fileName, out bool refIsValid)
        //{
        //    refIsValid = fileName.Contains(mimshakType);
        //    return refIsValid;
        //}

        //public string GetFileName(HttpPostedFile file)
        //{
        //    string filename = string.Empty;
        //    try
        //    {
        //        if (HttpContext.Current.Request.Files.Count > 0)
        //        {
        //            filename = file.FileName;
        //            if (HttpContext.Current.Request.Browser.Type.ToUpper().Contains("IE")
        //                || HttpContext.Current.Request.Browser.Type.ToUpper().Contains("MOZILLA")
        //                || HttpContext.Current.Request.Browser.Type.ToUpper().Contains("INTERNETEXPLORER"))
        //            {
        //                int index = filename.LastIndexOf('\\');
        //                filename = filename.Substring(index + 1);
        //            }
        //        }
        //        return filename;
        //    }
        //    catch (Exception ex)
        //    {
        //        //logger.Error(ex);
        //        return null;
        //    }
        //}

        //public bool CheckSize(int FileSize, int MaxFileSize)
        //{
        //    return ((FileSize / 1024f) < MaxFileSize);

        //}

        //public byte[] ConvertInputStreamToByte(HttpPostedFile file)
        //{
        //    //todo: close the file
        //    var buf = new byte[file.InputStream.Length];
        //    file.InputStream.Read(buf, 0, (int)file.InputStream.Length);
        //    return buf;

        //}

        //#region define MimeTypes readonly
        //private static readonly byte[] BMP = { 66, 77 };
        //private static readonly byte[] DOC = { 208, 207, 17, 224, 161, 177, 26, 225 };
        //private static readonly byte[] EXE_DLL = { 77, 90 };
        //private static readonly byte[] GIF = { 71, 73, 70, 56 };
        //private static readonly byte[] ICO = { 0, 0, 1, 0 };
        //private static readonly byte[] JPG = { 255, 216, 255 };
        //private static readonly byte[] MP3 = { 255, 251, 48 };
        //private static readonly byte[] OGG = { 79, 103, 103, 83, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0 };
        //private static readonly byte[] PDF = { 37, 80, 68, 70, 45, 49, 46 };
        //private static readonly byte[] PNG = { 137, 80, 78, 71, 13, 10, 26, 10, 0, 0, 0, 13, 73, 72, 68, 82 };
        //private static readonly byte[] RAR = { 82, 97, 114, 33, 26, 7, 0 };
        //private static readonly byte[] SWF = { 70, 87, 83 };
        //private static readonly byte[] TIFF = { 73, 73, 42, 0 };
        //private static readonly byte[] TORRENT = { 100, 56, 58, 97, 110, 110, 111, 117, 110, 99, 101 };
        //private static readonly byte[] TTF = { 0, 1, 0, 0, 0 };
        //private static readonly byte[] WAV_AVI = { 82, 73, 70, 70 };
        //private static readonly byte[] WMV_WMA = { 48, 38, 178, 117, 142, 102, 207, 17, 166, 217, 0, 170, 0, 98, 206, 108 };
        //private static readonly byte[] ZIP_DOCX = { 80, 75, 3, 4 };
        //#endregion MimeTypes readonly

        //public static string GetMimeType(byte[] file, string fileName, out string type)
        //{
        //    string mime = ""; //DEFAULT UNKNOWN MIME TYPE
        //    type = null;

        //    //Ensure that the filename isn't empty or null
        //    if (string.IsNullOrWhiteSpace(fileName))
        //    {
        //        return null;
        //    }

        //    //Get the file extension
        //    string extension = Path.GetExtension(fileName) == null
        //                           ? string.Empty
        //                           : Path.GetExtension(fileName).ToUpper();

        //    //Get the MIME Type
        //    if (file.Take(2).SequenceEqual(BMP))
        //    {
        //        type = "img";
        //        mime = "image/bmp";
        //    }
        //    else if (file.Take(8).SequenceEqual(DOC))
        //    {
        //        type = "??";
        //        mime = "application/msword";
        //    }
        //    else if (file.Take(2).SequenceEqual(EXE_DLL))
        //    {
        //        type = "excel";
        //        mime = "application/x-msdownload"; //both use same mime type
        //    }
        //    else if (file.Take(4).SequenceEqual(GIF))
        //    {
        //        type = "img";
        //        mime = "image/gif";
        //    }
        //    else if (file.Take(4).SequenceEqual(ICO))
        //    {
        //        type = "img";
        //        mime = "image/x-icon";
        //    }
        //    else if (file.Take(3).SequenceEqual(JPG))
        //    {
        //        type = "img";
        //        mime = "image/jpeg";
        //    }
        //    else if (file.Take(3).SequenceEqual(MP3))
        //    {
        //        type = "audio";
        //        mime = "audio/mpeg";
        //    }
        //    else if (file.Take(14).SequenceEqual(OGG))
        //    {
        //        if (extension == ".OGX")
        //        {
        //            type = "??";
        //            mime = "application/ogg";
        //        }
        //        else if (extension == ".OGA")
        //        {
        //            type = "audio";
        //            mime = "audio/ogg";
        //        }
        //        else
        //        {
        //            type = "video";
        //            mime = "video/ogg";
        //        }
        //    }
        //    else if (file.Take(7).SequenceEqual(PDF))
        //    {
        //        type = "pdf";
        //        mime = "application/pdf";
        //    }
        //    else if (file.Take(16).SequenceEqual(PNG))
        //    {
        //        type = "img";
        //        mime = "image/png";
        //    }
        //    else if (file.Take(7).SequenceEqual(RAR))
        //    {
        //        type = "??";
        //        mime = "application/x-rar-compressed";
        //    }
        //    else if (file.Take(3).SequenceEqual(SWF))
        //    {
        //        type = "??";
        //        mime = "application/x-shockwave-flash";
        //    }
        //    else if (file.Take(4).SequenceEqual(TIFF))
        //    {
        //        type = "img";
        //        mime = "image/tiff";
        //    }
        //    else if (file.Take(11).SequenceEqual(TORRENT))
        //    {
        //        type = "??";
        //        mime = "application/x-bittorrent";
        //    }
        //    else if (file.Take(5).SequenceEqual(TTF))
        //    {
        //        type = "??";
        //        mime = "application/x-font-ttf";
        //    }
        //    else if (file.Take(4).SequenceEqual(WAV_AVI))
        //    {
        //        type = "video";
        //        mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
        //    }
        //    else if (file.Take(16).SequenceEqual(WMV_WMA))
        //    {
        //        type = "video";
        //        mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
        //    }
        //    else if (file.Take(4).SequenceEqual(ZIP_DOCX))
        //    {
        //        type = "excel";
        //        mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" :
        //              (extension == ".XLS" || extension == ".XLSX" ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/x-zip-compressed");
        //    }

        //    return mime;
        //}

        //public static bool ValidateMimeType(byte[] file, string fileName, bool isImg, bool IsExcel, bool isPdf, bool isExe)
        //{
        //    string mime = ""; //DEFAULT UNKNOWN MIME TYPE

        //    //Ensure that the filename isn't empty or null
        //    if (string.IsNullOrWhiteSpace(fileName))
        //    {
        //        return false;
        //    }

        //    //Get the file extension
        //    string extension = Path.GetExtension(fileName) == null
        //                           ? string.Empty
        //                           : Path.GetExtension(fileName).ToUpper();

        //    //Get the MIME Type
        //    if (isImg && file.Take(2).SequenceEqual(BMP))
        //    {
        //        mime = "image/bmp";
        //    }
        //    else if (false && file.Take(8).SequenceEqual(DOC))
        //    {
        //        mime = "application/msword";
        //    }
        //    else if (isExe && file.Take(2).SequenceEqual(EXE_DLL))
        //    {
        //        mime = "application/x-msdownload"; //both use same mime type
        //    }
        //    else if (isImg && file.Take(4).SequenceEqual(GIF))
        //    {
        //        mime = "image/gif";
        //    }
        //    else if (false && file.Take(4).SequenceEqual(ICO))
        //    {
        //        mime = "image/x-icon";
        //    }
        //    else if (isImg && file.Take(3).SequenceEqual(JPG))
        //    {
        //        mime = "image/jpeg";
        //    }
        //    else if (false && file.Take(3).SequenceEqual(MP3))
        //    {
        //        mime = "audio/mpeg";
        //    }
        //    else if (false && file.Take(14).SequenceEqual(OGG))
        //    {
        //        if (extension == ".OGX")
        //        {
        //            mime = "application/ogg";
        //        }
        //        else if (extension == ".OGA")
        //        {
        //            mime = "audio/ogg";
        //        }
        //        else
        //        {
        //            mime = "video/ogg";
        //        }
        //    }
        //    else if (isPdf && file.Take(7).SequenceEqual(PDF))
        //    {
        //        mime = "application/pdf";
        //    }
        //    else if (isImg && file.Take(16).SequenceEqual(PNG))
        //    {
        //        mime = "image/png";
        //    }
        //    else if (false && file.Take(7).SequenceEqual(RAR))
        //    {
        //        mime = "application/x-rar-compressed";
        //    }
        //    else if (false && file.Take(3).SequenceEqual(SWF))
        //    {
        //        mime = "application/x-shockwave-flash";
        //    }
        //    else if (isImg && file.Take(4).SequenceEqual(TIFF))
        //    {
        //        mime = "image/tiff";
        //    }
        //    else if (false && file.Take(11).SequenceEqual(TORRENT))
        //    {
        //        mime = "application/x-bittorrent";
        //    }
        //    else if (false && file.Take(5).SequenceEqual(TTF))
        //    {
        //        mime = "application/x-font-ttf";
        //    }
        //    else if (false && file.Take(4).SequenceEqual(WAV_AVI))
        //    {
        //        mime = extension == ".AVI" ? "video/x-msvideo" : "audio/x-wav";
        //    }
        //    else if (false && file.Take(16).SequenceEqual(WMV_WMA))
        //    {
        //        mime = extension == ".WMA" ? "audio/x-ms-wma" : "video/x-ms-wmv";
        //    }
        //    else if (IsExcel && file.Take(4).SequenceEqual(ZIP_DOCX))
        //    {
        //        mime = extension == ".DOCX" ? "application/vnd.openxmlformats-officedocument.wordprocessingml.document" :
        //              (extension == ".XLS" || extension == ".XLSX" ? "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" : "application/x-zip-compressed");
        //    }

        //    return mime != string.Empty;
        //}

        //public static bool ValidateMimeType(Stream stream, string fileName, bool isImg, bool IsExcel, bool isPdf, bool isExe)
        //{
        //    byte[] buffer = new byte[16 * 1024];
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        int read;
        //        while ((read = stream.Read(buffer, 0, buffer.Length)) > 0)
        //        {
        //            ms.Write(buffer, 0, read);
        //        }

        //    }
        //    return ValidateMimeType(buffer, fileName, isImg, IsExcel, isPdf, isExe);
        //}

    }
}
