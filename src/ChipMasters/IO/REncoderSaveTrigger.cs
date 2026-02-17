using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using System.IO;
using System.Threading.Tasks;

namespace ChipMasters.IO
{
    /// <summary>
    /// <see cref="ISaveTrigger"/> that saves using a <see cref="IEncoder{T}"/> instance.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class REncoderSaveTrigger<T> : ISaveTrigger
    {
        private readonly string _mainPath;
        private readonly string _backupPath;
        private readonly string _tempPath;
        private readonly IEncoder<T> _encoder;
        private readonly T _data;

        private bool _isClosed = false;
        private Task _backgroudSave = Task.CompletedTask;



        /// <inheritdoc/>
        public REncoderSaveTrigger(string path, IEncoder<T> encoder, T data, string backupExt = NApplicationDataManager.BACKUP_EXTENSION, string tempExtension = ".tmp")
        {
            _mainPath = path.AssertNotNull();
            _backupPath = $"{_mainPath}{backupExt.AssertNotNull()}";
            _tempPath = $"{_mainPath}{tempExtension.AssertNotNull()}";
            _encoder = encoder.AssertNotNull();
            _data = data;
        } // end ctor



        /// <inheritdoc/>
        public void Save()
        {
            if (_isClosed)
                return;

            if (_backgroudSave.IsCompleted)
                _backgroudSave = Write(); // only save if save is not ongoing. Could wait for prior save, but that risks an infinite chain of await writes, where the data written is far out of date.
        } // end Save()

        /// <inheritdoc/>
        public async Task Close()
        {
            if (_isClosed)
                return;

            _isClosed = true;
            await _backgroudSave;
            await Write();
        } // end Close();



        private async Task Write()
        {
            string fileContent = _encoder
                .Encode(JsonStreamCoder.INSTANCE, _data, SIOUtil.CODING_SETTINGS)
                .ToString(SIOUtil.JSON_FORMATTING);

            using (StreamWriter sw = new(_tempPath))
                await sw.WriteAsync(fileContent); // start writing to temp file

            if (File.Exists(_mainPath)) // replace backup with most current
                File.Copy(_mainPath, _backupPath, overwrite: true);

            File.Move(_tempPath, _mainPath, overwrite: true); // replace current save with temp file containing the most recent data.
        } // end Write()
    } // end class
} // end namespace