namespace Web_API.Utilities
{

    public static class ContentTypeMapper
    {
        public static string? GetMimeType(string fileName)
        {
            string extension = Path.GetExtension(fileName).ToLowerInvariant();
            return _mimeTypes.TryGetValue(extension, out string mimeType) ? mimeType : null;
        }

        private static readonly Dictionary<string, string> _mimeTypes = new Dictionary<string, string>
    {
        { ".aac", "audio/aac" },
        { ".abw", "application/x-abiword" },
        { ".arc", "application/octet-stream" },
        { ".avi", "video/x-msvideo" },
        { ".azw", "application/vnd.amazon.ebook" },
        { ".bin", "application/octet-stream" },
        { ".bz", "application/x-bzip" },
        { ".bz2", "application/x-bzip2" },
        { ".csh", "application/x-csh" },
        { ".css", "text/css" },
        { ".csv", "text/csv" },
        { ".doc", "application/msword" },
        { ".epub", "application/epub+zip" },
        { ".gif", "image/gif" },
        { ".htm", "text/html" },
        { ".html", "text/html" },
        { ".ico", "image/x-icon" },
        { ".ics", "text/calendar" },
        { ".jar", "application/java-archive" },
        { ".jpeg", "image/jpeg" },
        { ".jpg", "image/jpeg" },
        { ".png", "image/png" },
        { ".js", "application/javascript" },
        { ".json", "application/json" },
        { ".mid", "audio/midi" },
        { ".midi", "audio/midi" },
        { ".mpeg", "video/mpeg" },
        { ".mpkg", "application/vnd.apple.installer+xml" },
        { ".odp", "application/vnd.oasis.opendocument.presentation" },
        { ".ods", "application/vnd.oasis.opendocument.spreadsheet" },
        { ".odt", "application/vnd.oasis.opendocument.text" },
        { ".oga", "audio/ogg" },
        { ".ogv", "video/ogg" },
        { ".ogx", "application/ogg" },
        { ".pdf", "application/pdf" },
        { ".ppt", "application/vnd.ms-powerpoint" },
        { ".rar", "application/x-rar-compressed" },
        { ".rtf", "application/rtf" },
        { ".sh", "application/x-sh" },
        { ".svg", "image/svg+xml" },
        { ".swf", "application/x-shockwave-flash" },
        { ".tar", "application/x-tar" },
        { ".tif", "image/tiff" },
        { ".tiff", "image/tiff" },
        { ".ttf", "font/ttf" },
        { ".vsd", "application/vnd.visio" },
        { ".wav", "audio/wav" },
        { ".txt", "text/plain" },
        { ".xml", "application/xml" },
        { ".webm", "video/webm" },
        { ".mp4", "video/mp4" },
        { ".ogg", "audio/ogg" },
        { ".mp3", "audio/mpeg" },
        { ".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet" },
        { ".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation" },
        { ".webp", "image/webp" },
        { ".woff", "font/woff" },
        { ".woff2", "font/woff2" }
        // Agrega aquí otros tipos de contenido según sea necesario
    };


    }
}
