using ChipMasters.GodotWrappers;
using ChipMasters.User;
using Godot;
using GSR.EnDecic;
using GSR.EnDecic.Jsonic;
using GSR.Jsonic;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Threading.Tasks;

namespace ChipMasters.IO
{
    /// <summary>
    /// Godot <see cref="Node"/> based game load screen logic.
    /// </summary>
    public partial class NApplicationDataManager : NNode
    {
        private static readonly string SAVES_DIR = ProjectSettings.GlobalizePath($"user://saves/");
        private static readonly string USER_P1_PATH = $"{SAVES_DIR}user.json";
        private static readonly string USER_P2_PATH = $"{SAVES_DIR}_user.json";
        /// <summary>
        /// Text added to the end of a save path to indicate it's the backup file that will be fallen back on.
        /// </summary>
        public const string BACKUP_EXTENSION = ".backup";
        private const int AUTO_SAVE_INTERVAL = 12000; // milliseconds



        /// <summary>
        /// Singleton <see cref="NApplicationDataManager"/> instance.
        /// </summary>
        public static NApplicationDataManager INSTANCE => _instance ?? throw new RNotReadyException();
        private static NApplicationDataManager? _instance;



        /// <summary>
        /// The user's account, game currently only supports a single save.
        /// </summary>
        public IUser User => _user ?? throw new RNotReadyException();
        private readonly IUser _user;

        /// <summary>
        /// Has the load phase completed.
        /// </summary>
        public bool IsLoaded { get; private set; } = false;



        /// <inheritdoc/>
        public NApplicationDataManager()
        {
            if (_instance is not null)
                throw new InvalidOperationException("Data manager can only be instantiated once.");
            _instance = this;
            _user = Load(USER_P1_PATH, IUser.P1_ENDEC, () => new RUser()); // load file synchronously
#if DEBUG
            GD.Print($"Trying to load user from: \"{USER_P1_PATH}\"");
#endif

            Load(USER_P2_PATH, _user, IUser.P2_ENAPDEC);
            ISaveTrigger userP1SaveTrigger = new REncoderSaveTrigger<IUser>(USER_P1_PATH, IUser.P1_ENDEC, _user); // create object which will coordinate background saving.
            ISaveTrigger userP2SaveTrigger = new REncoderSaveTrigger<IUser>(USER_P2_PATH, IUser.P2_ENAPDEC, _user);


            // attach save triggers
#pragma warning disable CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            AutoSave(userP1SaveTrigger, AUTO_SAVE_INTERVAL);
            AutoSave(userP2SaveTrigger, AUTO_SAVE_INTERVAL);
#pragma warning restore CS4014 // Because this call is not awaited, execution of the current method continues before the call is completed
            NApplicationManager.INSTANCE.OnClosing += userP1SaveTrigger.Close; // assure user synchronously saved when application ends.
            NApplicationManager.INSTANCE.OnClosing += userP2SaveTrigger.Close;

            IsLoaded = true;
        } // end ctor



        private async Task AutoSave(ISaveTrigger saveTrigger, int interval)
        {
            while (true)
            {
                saveTrigger.Save();
                await Task.Delay(interval);
            }
        } // end AutoSave()




        private static void Load<T>(string path, T t, IApplicativeDecoder<T> decoder, string backupExt = BACKUP_EXTENSION)
            where T : class
        {
            if (!TryLoad(path, t, decoder))
                TryLoad($"{path}{backupExt}", t, decoder);
        } // end Load()

        private static bool TryLoad<T>(string path, T t, IApplicativeDecoder<T> decoder)
            where T : class
        {
            EstablishDir(path);
            if (!File.Exists(path))
                return false;

            using StreamReader reader = new(path);
            string text = reader.ReadToEnd();
            try
            {
                decoder.Apply(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(text), t, SIOUtil.CODING_SETTINGS);
                return true;
            }
            catch (Exception e)
            {
#if DEBUG
                GD.PrintErr($"Save file corrupted: \"{path}\", failed with error: {e.Message}.\r\n\r\n{text}");
#endif
                return false;
            }
        } // end TryLoad()









        private static T Load<T>(string path, IDecoder<T> decoder, Func<T> defaultSupplier, string backupExt = BACKUP_EXTENSION)
            where T : class
        {
            if (TryLoad(path, decoder, out T? main))
                return main;
            else if (TryLoad($"{path}{backupExt}", decoder, out T? backup))
                return backup;

            return defaultSupplier();
        } // end Load()

        private static bool TryLoad<T>(string path, IDecoder<T> decoder, [NotNullWhen(true)] out T? data)
            where T : class
        {
            data = null;

            EstablishDir(path);

            if (!File.Exists(path))
                return false;

            using StreamReader reader = new(path);
            string text = reader.ReadToEnd();
            try
            {
                data = decoder.Decode(JsonStreamCoder.INSTANCE, JsonElement.ParseJson(text), SIOUtil.CODING_SETTINGS);
                return true;
            }
            catch (Exception e)
            {
#if DEBUG
                GD.PrintErr($"Save file corrupted: \"{path}\", failed with error: {e.Message}.\r\n\r\n{text}");
#endif
                return false;
            }
        } // end TryLoad()



        /// <summary>
        /// Assure the directories of the path exist.
        /// </summary>
        /// <param name="path"></param>
        private static void EstablishDir(string path)
        {
            string? dirPath = Path.GetDirectoryName(path);
            if (dirPath is null)
                return;

            if (!Directory.Exists(dirPath))
                Directory.CreateDirectory(dirPath);
        } // end EstablishDir()

    } // end class
} // end namespace
