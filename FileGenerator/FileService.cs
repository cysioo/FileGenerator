using System.Diagnostics.CodeAnalysis;

namespace FileGenerator
{
    public class FileService(IAppSettings appSettings) : IDisposable, IFileService
    {
        private bool disposedValue;
        private static string[]? _words;
        private StreamWriter? _fileWriter;

        public string[] Words
        {
            get
            {
                if (_words == null)
                {
                    _words = File.ReadAllLines("words_alpha.txt");
                }
                return _words;
            }
        }

        public string OutputFilePath { get; private set; }

        public void WriteLineToOutputFile(string line)
        {
            PrepareForWriting();

            _fileWriter.WriteLine(line);
        }

        [MemberNotNull(nameof(_fileWriter))]
        private void PrepareForWriting()
        {
            if (_fileWriter == null)
            {
                if (!Directory.Exists(appSettings.FileSaveLocation))
                {
                    Directory.CreateDirectory(appSettings.FileSaveLocation);
                }
                var fullPath = Path.Combine(appSettings.FileSaveLocation, appSettings.FileName);
                _fileWriter = new StreamWriter(fullPath);
                OutputFilePath = fullPath;
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    if (_fileWriter != null)
                    {
                        _fileWriter.Dispose();
                        _fileWriter = null;
                    }
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}
